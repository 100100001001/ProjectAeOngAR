using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 하단 버튼을 눌렀을 때 상태 변화
public class OnClickButtons : MonoBehaviour
{
    /// <summary>
    ///  말풍선
    /// </summary>
    [Header("--- 말풍선 ---")]
    public GameObject bubble;            // 말풍선 오브젝트를 담아두는 변수
    private RectTransform bubbleRT;      // 말풍선 위치가 랜덤으로 뜨기 위해 RectTransform을 받아오는 변수
    private Image bubbleImg;             // 말풍선 오브젝트의 Image를 불러오기 위한 변수

    public Sprite[] bubbleSprites;       // 말풍선 sprite들을 담아두는 변수

    private TextMeshProUGUI bubbleTMPro; // TextMeshPro를 담아두기 위한 변수
    private string bubbleText;           // TextMeshPro의 텍스트를 변경하기 위한 변수

    [Header("--- For the SmartButton / 공부 ---")]
    public GameObject readABook;         // 책 보는 이미지를 띄우기 위해, 이미지가 있는 오브젝트를 받을 변수

    [Header("--- For the CleanButton / 먼지 생성 ---")]
    public GameObject dusts;
    public GameObject timeFlowTest;

    [Header("--- For the SleepButton / 자는 얼굴 ---")]
    public GameObject players;
    public Texture2D sleepingFace;
    public GameObject BlackImage;
    private Texture originFace;
    int sleepN = 1;



    private void Start()
    {
        bubbleRT = bubble.GetComponent<RectTransform>();
        bubbleImg = bubble.GetComponent<Image>();

    }

    /// <summary>
    /// 샤워 버튼을 눌렀을 때
    /// </summary>
    public void TakeShower()
    {
        if (StatusBar.instance.curClean >= 100)      // Clean이 가득 찬 상태인데 샤워 버튼을 눌렀을 때
        {
            StartCoroutine(ThinkingBubble("Clean"));
            StatusBar.instance.HappyValue(false, 10); // 기분이 안 좋아짐
            return;
        }

        for (int i = 0; i < 9; i++) dusts.transform.GetChild(i).gameObject.SetActive(false);
        timeFlowTest.GetComponent<TimeFlowTest>().timeCnt = 0;


        StartCoroutine(RecBubble("Clean"));

        StatusBar.instance.CleanValue(true, 50);
        StatusBar.instance.HappyValue(true, 10);

        //Status.instance.RemoveDust();                 // 화면에 띄워진 먼지 제거

    }

    /// <summary>
    /// 공부 버튼을 눌렀을 때
    /// </summary>
    public void ReadToBook()
    {

        if (StatusBar.instance.curSmart >= 100)      // Smart가 가득 찬 상태인데 공부 버튼을 눌렀을 때
        {
            StartCoroutine(ThinkingBubble("Smart"));
            StatusBar.instance.HappyValue(false, 10); // 기분이 안 좋아짐
            return;
        }


        StartCoroutine(ReadToBookAnimation());
        StartCoroutine(RecBubble("Smart"));

        StatusBar.instance.SmartValue(true, 20);
        StatusBar.instance.HappyValue(true, 10);

    }

    /// <summary>
    /// 자는 버튼을 눌렀을 때
    /// </summary>
    public void Sleeping()
    {
        if (Status.instance.evo == Status.Evolution.BABY)
        {
            originFace = players.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].GetTexture("_MainTex");  // 본래의 Face 텍스쳐 저장
            players.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", sleepingFace); // 자는 Face 텍스쳐로 변경
        }
        else if (Status.instance.evo == Status.Evolution.CHILD)
        {
            originFace = players.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].GetTexture("_MainTex");  // 본래의 Face 텍스쳐 저장
            players.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", sleepingFace); // 자는 Face 텍스쳐로 변경
        }
        else if (Status.instance.evo == Status.Evolution.YOUTH)
        {
            originFace = players.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].GetTexture("_MainTex");  // 본래의 Face 텍스쳐 저장
            players.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", sleepingFace); // 자는 Face 텍스쳐로 변경
        }

        BlackImage.SetActive(true); // 어두운 패널 활성화
    }

    /// <summary>
    /// 어두워진 패널을 눌렀을 때. 잠 끝
    /// </summary>
    public void EndSleep()
    {
        if (Status.instance.evo == Status.Evolution.BABY)
        {
            players.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", originFace); // 본래의 Face 텍스쳐로 변경
        }
        else if (Status.instance.evo == Status.Evolution.CHILD)
        {
            players.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", originFace); // 본래의 Face 텍스쳐로 변경
        }
        else if (Status.instance.evo == Status.Evolution.YOUTH)
        {
            players.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", originFace); // 본래의 Face 텍스쳐로 변경
        }

        BlackImage.SetActive(false); // 어두운 패널 비활성화

        Status.instance.cntSleep1++;

    }

    /// <summary>
    /// 반려동물(캐릭터)가 생각하는 말풍선
    /// </summary>
    /// <param name="st">상태에 따라 달라지는 말풍선 텍스트를 구분하기 위한 매개변수</param>
    /// <returns></returns>
    IEnumerator ThinkingBubble(string st)
    {
        // 샤워했을 때
        if (st == "Clean")
        {
            string[] thinkingTextShower = new string[3];
            thinkingTextShower[0] = "왜..\n또 씻지?";
            thinkingTextShower[1] = "지도.. 씻고\n또 씻어라";
            thinkingTextShower[2] = "아 저\n깨끗하다고요";

            bubbleText = thinkingTextShower[Random.Range(0, 3)];
        }

        // 공부했을 때
        if (st == "Smart")
        {
            string[] thinkingTextShower = new string[2];
            thinkingTextShower[0] = "공부 다 했는데.";
            thinkingTextShower[1] = "지는.. \n공부 안하면서..";

            bubbleText = thinkingTextShower[Random.Range(0, 2)];
        }


        bubble.SetActive(true);

        bubbleImg.sprite = bubbleSprites[0];
        bubbleRT.anchoredPosition = new Vector3(Random.Range(-200, 200), Random.Range(-380, 280), 0);

        bubble.GetComponentInChildren<TextMeshProUGUI>().text = bubbleText;

        yield return new WaitForSeconds(3f);
        bubble.SetActive(false);
    }

    /// <summary>
    /// 반려동물(캐릭터)의 말풍선
    /// </summary>
    /// <param name="st">상태에 따라 달라지는 말풍선 텍스트를 구분하기 위한 매개변수</param>
    /// <returns></returns>
    IEnumerator RecBubble(string st)
    {
        // 샤워했을 때
        if (st == "Clean")
        {
            string[] thinkingTextShower = new string[3];
            thinkingTextShower[0] = "히히\n깨끗해";
            thinkingTextShower[1] = "기분 조아\n히히";
            thinkingTextShower[2] = "나는\n뽀송해~!";

            bubbleText = thinkingTextShower[Random.Range(0, 3)];
        }

        // 공부했을 때
        if (st == "Smart")
        {
            string[] thinkingTextShower = new string[3];
            thinkingTextShower[0] = "똑똑해지는 기분\n너무 좋아!";
            thinkingTextShower[1] = "난 똑똑해!\n난 멋져!";
            thinkingTextShower[2] = "내가 다 읽어야지~ 히히";

            bubbleText = thinkingTextShower[Random.Range(0, 3)];
        }


        bubble.SetActive(true);

        bubbleImg.sprite = bubbleSprites[1];
        bubbleRT.anchoredPosition = new Vector3(Random.Range(-200, 200), Random.Range(-380, 280), 0);

        bubble.GetComponentInChildren<TextMeshProUGUI>().text = bubbleText;

        yield return new WaitForSeconds(3f);
        bubble.SetActive(false);
    }

    /// <summary>
    /// 책 읽는 이미지 오브젝트를 활성화
    /// </summary>
    /// <returns></returns>
    IEnumerator ReadToBookAnimation()
    {
        readABook.SetActive(true);
        yield return new WaitForSeconds(3f);
        readABook.SetActive(false);
    }


}
