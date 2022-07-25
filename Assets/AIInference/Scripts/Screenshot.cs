using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Screenshot : MonoBehaviour
{
    bool isCoroutinePlaying;  // 코루틴 중복방지
    //static WebCamTexture cam; // PC 확인용 캠 
    
    [Header("Debug")]
    public TextMeshProUGUI debugUI; // 확인용 텍스트
    public int debugnum = 1;        // 오류 확인하기 위해

    public Sprite captureCompleteSp;  // 캡처 후 모델 예측할 버튼의 sprite를 변경하기 위해 받아 줌
    public GameObject learningButton; // 모델 예측할 버튼
    public GameObject shotButton;     // 캡처 버튼

    // 캡쳐 버튼을 누르면 호출
    public void Capture_Button()
    {
        // 중복 방지, true일 때 실행
        if (!isCoroutinePlaying)
        {
            captureScreenshot();

            // 모델 예측 버튼을 누르기 전까진 캡처가 한번만 되기 위해 shotButton의 interactable를 비활성화해줌
            shotButton.GetComponent<Button>().interactable = false;

            // 캡처 버튼을 누르면 모델 예측 버튼의 sprite가 변경되고, interactable가 활성화 되어 버튼 기능 가능
            learningButton.GetComponent<Image>().sprite = captureCompleteSp;
            learningButton.GetComponent<Button>().interactable = true;
        }
    }

    void captureScreenshot()
    {
        isCoroutinePlaying = true;

        // 스크린샷
        StartCoroutine(ScreenshotAndGallery());
        isCoroutinePlaying = false;

        debugnum++;
    }

    IEnumerator ScreenshotAndGallery() 
    {
        // 하나의 프레임이 완전히 종료될 때 호출
        yield return new WaitForEndOfFrame();
        // 스크린샷할 이미지 담을 공간 생성
        Texture2D screenShot = new Texture2D(500, 500, TextureFormat.RGB24, false); //카메라가 인식할 영역의 크기
        
        // 현재 이미지로부터 지정 영역의 픽셀들을 텍스처에 저장
        Rect area = new Rect(479, 294, 500, 500); // (cameraview UI Pivot 좌하단 기준) Rect(좌표 x,y 입력, 가로 길이, 세로 길이)
        screenShot.ReadPixels(area, 0, 0); 
        screenShot.Apply();

        GetInferenceFromModel.texture = screenShot;  // 찍은 사진을 따로 저장하지 않고 GetInferenceFromModel texture로 넘겨줌
    }

}
