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
    public TextMeshProUGUI debugUI;
    public int debugnum = 1;





    public Sprite captureCompleteSp;
    public GameObject learningButton;
    public GameObject ShotButton;



    // 캡쳐 버튼을 누르면 호출
    public void Capture_Button()
    {
        ShotButton.GetComponent<Button>().interactable = true;


        // 중복방지 bool, true 일 때 실행
        if (!isCoroutinePlaying)
        {
            StartCoroutine("captureScreenshot");

            ShotButton.GetComponent<Button>().interactable = false;

            learningButton.GetComponent<Image>().sprite = captureCompleteSp;
            learningButton.GetComponent<Button>().interactable = true;


        }
    }

    IEnumerator captureScreenshot()
    {
        isCoroutinePlaying = true;
        // 스크린샷 
        StartCoroutine(ScreenshotAndGallery());

        // 셔터 사운드 넣기...
        yield return new WaitForSeconds(1f);

        isCoroutinePlaying = false;

        debugnum++;

    }

    IEnumerator ScreenshotAndGallery() 
    {
        yield return new WaitForEndOfFrame();
         //스크린샷할 이미지 담을 공간 생성
        Texture2D screenShot = new Texture2D(500, 500, TextureFormat.RGB24, false); //카메라가 인식할 영역의 크기
        
        // 현재 이미지로부터 지정 영역의 픽셀들을 텍스쳐에 저장
        Rect area = new Rect(479, 294, 500, 500); // (cameraview UI Pivot 좌하단 기준) Rect(좌표 x,y 입력, 가로 길이, 세로 길이)
        screenShot.ReadPixels(area, 0, 0); 
        screenShot.Apply();

        GetInferenceFromModel.texture = screenShot;  // 찍은 사진을 따로 저장하지 않고 GetInferenceFromModel texture로 넘겨줌
    }

}
