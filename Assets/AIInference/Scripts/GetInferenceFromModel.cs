using System;          // Serializable, Array
using System.Linq;     // Max()
using Unity.Barracuda; // Model, NNModel, IWorker
using UnityEngine;     // MonoBehaviour 
using TMPro;           // TextMeshProUGUI
using UnityEditor;     // SetTextureImporterFormat()
using UnityEngine.UI;  // Canvas의 UI를 사용하기 위해 선언

// 모델의 추론 과정을 모방한 클래스 
// 0 : Red, 1 : Green, 2 : Blue, 3 : White, 4 : Pink
public class GetInferenceFromModel : MonoBehaviour
{
    public static GetInferenceFromModel instance; // 인스턴스화
    public static Texture2D texture;              // 모델이 예측할 이미지 텍스처

    [Header(" ---Model---")]
    public NNModel modelAsset;     // 학습된 모델
    private Model _runtimeModel;   // 실행할 모델
    private IWorker _engine;       // 모델을 돌릴 엔진
    public static int resultValue = -1; // 결과 값
    public Prediction prediction;  // 예측값 구조체를 통해 필요한 기능 받아오기 

    [Header("---Debugger---")]
    public TextMeshProUGUI resultText; // 결과값을 확인할 UI Text
    public RawImage testImage;         // 캡처 후 확인할 RawImage
    public RawImage testImageAfter;    // 이미지 전처리 후 확인할 RawImage

    [Header("------")]
    public Sprite[] predictionCompleteSp; // 예측 후 Sprite 변경을 위해 변수 선언
    public GameObject shotButton;         // 캡처하는 버튼
    public GameObject completePanel;      // 예측이 끝나고 띄워질 panel 오브젝트

    

    /// <summary>
    /// 인스펙터에서 쉽게 볼 수 있는 방식으로 예측 결과를 유지하는 데 사용되는 구조체
    /// </summary>
    [Serializable]
    public struct Prediction
    {
        public int predictedValue; // 모델의 예측가능성 중 가장 높은 값
        public float[] predicted;  // 라벨에 대한 예측값 배열

        // 텐서를 매개변수로 받아 예측값을 가져오는 메서드
        public void SetPrediction(Tensor tens)
        {
            // 부동 소수점 값 출력을 예측 배열로 추출
            predicted = tens.AsFloats();
            // 가장 가능성이 높은 것(=예측 값)의 인덱스 가져오기 
            predictedValue = Array.IndexOf(predicted, predicted.Max());
            Debug.Log($"Predicted {predictedValue}");
        }

    }

    // 싱글톤 패턴을 위한 인스턴스화
    private void Awake() 
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        // 런타임 모델 및 작업자를 설정
        _runtimeModel = ModelLoader.Load(modelAsset);
        // 실행 엔진 설정(worker 생성 (실행 모델, 실행할 엔진 : CPU, GPU, Auto))
        _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.CPU);
        // 예측 구조체 인스턴스화
        prediction = new Prediction();
        // 확인용 텍스트
        resultText.text = "카메라가 켜졌습니다."; 

        completePanel.SetActive(false);                                    // 예측 완료 후 나와야하는 panel이기 때문에 비활성화
        gameObject.GetComponent<Image>().sprite = predictionCompleteSp[1]; // 기본 sprite로 변경
    }

    // ModelExecute 버튼 누르면 실행될 함수
    public void PreModel()
    {
        // 색상을 찾는 sprite로 변경
        gameObject.GetComponent<Image>().sprite = predictionCompleteSp[0];

        // 예측 완료 패널 활성화
        completePanel.SetActive(true);

        // shotButton이 활성화되어야 모델 예측이 가능하기 때문에
        // shotButton은 interactable 활성화, 모델 예측 오브젝트의 버튼은 interactable 비활성화
        shotButton.GetComponent<Button>().interactable = true;
        gameObject.GetComponent<Button>().interactable = false;

        resultText.text = "PreModel"; // 확인용 텍스트

        testImage.texture = texture; // 이미지 확인을 위해 테스트 이미지 텍스처 변경
        texture = ScaleTexture(texture, 0.448f); // 이미지 전처리 : 리사이즈
  
        testImageAfter.texture = texture; ; // 이미지 확인을 위해 테스트 이미지 텍스처 변경

        var channelCount = 3; //1 = 회색조, 3 = 색상, 4 = 색상 알파
        // 텍스처에서 입력을 위한 텐서 생성
        Tensor inputX = new Tensor(texture, channelCount); //(0, 244, 244, 3) - 모델 학습 사이즈에 맞춰 생성
        // 실행해서(Execute) 결과값 내보내기(PeekOutput)
        Tensor outputY = _engine.Execute(inputX).PeekOutput();
        // 출력 텐서를 사용하여 예측 구조체의 값을 설정
        prediction.SetPrediction(outputY);

        resultValue = prediction.predictedValue;

        // 예측값중 가장 높은 값 문자열로 변환해서 UI Text에 보여주기
        TextValue(prediction.predictedValue);
        // 입력 텐서를 수동으로 폐기(가비지 컬렉터 아님)
        inputX.Dispose();
    }

    // 비율을 통한 Resize하기
    public Texture2D ScaleTexture(Texture2D source, float _scaleFactor)
    {
        if (_scaleFactor == 1f)
        {
            return source;
        }
        else if (_scaleFactor == 0f)
        {
            return Texture2D.blackTexture;
        }

        int _newWidth = Mathf.RoundToInt(source.width * _scaleFactor);
        int _newHeight = Mathf.RoundToInt(source.height * _scaleFactor);
        
        Color[] _scaledTexPixels = new Color[_newWidth * _newHeight];

        for (int _yCord = 0; _yCord < _newHeight; _yCord++)
        {
            float _vCord = _yCord / (_newHeight * 1f);
            int _scanLineIndex = _yCord * _newWidth;

            for (int _xCord = 0; _xCord < _newWidth; _xCord++)
            {
                float _uCord = _xCord / (_newWidth * 1f);

                _scaledTexPixels[_scanLineIndex + _xCord] = source.GetPixelBilinear(_uCord, _vCord);
            }
        }

        // 스케일 텍스처 생성
        Texture2D result = new Texture2D(_newWidth, _newHeight, source.format, false);

        result.SetPixels(_scaledTexPixels, 0);
        result.Apply();
        return result;
    }

    private void OnDestroy()
    {
        // 엔진을 수동으로 폐기합니다(가비지 컬렉터 아님)
        _engine?.Dispose();
    }

    // 확인용 텍스트
    private void TextValue(int value)
    {
        resultText.text = "" + value;
    }
}


