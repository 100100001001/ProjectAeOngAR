using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("--- For the ShowerButton ---")]
    public GameObject shower;

    [Header("--- For the GameButton ---")]
    public TextMeshProUGUI playGameText;
    public TextMeshProUGUI FindColorText;


    private void Start()
    {
        bubbleRT = bubble.GetComponent<RectTransform>();
        bubbleImg = bubble.GetComponent<Image>();

        bubbleTMPro = bubble.GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// 샤워 버튼을 눌렀을 때
    /// </summary>
    public void TakeShower()
    {
        if (StatusBar.instance.curClean >= 100)       // Clean이 가득 찬 상태인데 샤워 버튼을 눌렀을 때
        {
            //Status.instance.dustCnt = 0;
            StartCoroutine(ThinkingBubble("Clean"));
            StatusBar.instance.HappyValue(false, 10); // 기분이 안 좋아짐
            return;
        }

        for (int i = 0; i < 9; i++) dusts.transform.GetChild(i).gameObject.SetActive(false); // 먼지 비활성화


        StartCoroutine(RecBubble("Clean"));
        StartCoroutine(TakeAShowerAnimation());

        StatusBar.instance.CleanValue(true, 50);
        StatusBar.instance.HappyValue(true, 10);
        StatusBar.instance.EnergyValue(false, 5);

        Status.instance.cntClean1++;

        Status.instance.dustCnt = 0;
        //Status.instance.dustCnt /= 2;



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
        StatusBar.instance.EnergyValue(false, 10);


        Status.instance.cntSmart1++;


    }

    /// <summary>
    /// 자는 버튼을 눌렀을 때
    /// </summary>
    public void Sleeping()
    {
        if (Status.instance.evo1 == Status.Evolution1.BABY)
        {
            originFace = players.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].GetTexture("_MainTex");  // 본래의 Face 텍스쳐 저장
            players.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", sleepingFace); // 자는 Face 텍스쳐로 변경
        }
        else if (Status.instance.evo1 == Status.Evolution1.CHILD)
        {
            originFace = players.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].GetTexture("_MainTex");  // 본래의 Face 텍스쳐 저장
            players.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", sleepingFace); // 자는 Face 텍스쳐로 변경
        }
        else if (Status.instance.evo1 == Status.Evolution1.YOUTH)
        {
            originFace = players.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].GetTexture("_MainTex");  // 본래의 Face 텍스쳐 저장
            players.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", sleepingFace); // 자는 Face 텍스쳐로 변경
        }

        BlackImage.SetActive(true); // 어두운 패널 활성화
    }

    /// <summary>
    /// 어두워진 패널을 눌렀을 때. 잠 끝
    /// </summary>
    public void EndSleep()
    {
        if (Status.instance.evo1 == Status.Evolution1.BABY)
        {
            players.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", originFace); // 본래의 Face 텍스쳐로 변경
        }
        else if (Status.instance.evo1 == Status.Evolution1.CHILD)
        {
            players.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", originFace); // 본래의 Face 텍스쳐로 변경
        }
        else if (Status.instance.evo1 == Status.Evolution1.YOUTH)
        {
            players.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", originFace); // 본래의 Face 텍스쳐로 변경
        }

        BlackImage.SetActive(false); // 어두운 패널 비활성화

        Status.instance.cntSleep1++;

    }

    public void PlayButton()
    {
        playGameText.text = "화면을 터치하면 총알이 나와요!\n점수에 따라 선물을 받을지도~";
    }

    public void PlayGame()
    {
        if (StatusBar.instance.curEnergy >= 17) SceneManager.LoadScene("MainCopy_Game_Bomb");
        else
        {
            playGameText.text = "또바기가 힘들어서 못 하겠대요.\n기력..71력..17..\n기력이 17 이상은 되어야 하지 않을까요?";
        }

    }
    
    public void FindColorButton()
    {
        FindColorText.text = "화면을 고정하고 찰칵!\n똑똑한 AI가 또바기의 색을 바꿔줘요";
    }

    public void PlayFindColor()
    {
        if (StatusBar.instance.curHappy >= 100) SceneManager.LoadScene("MainCopy_FindColor");
        else
        {
            FindColorText.text = "또바기가 최고로 행복할 때만\n색을 찾을 수 있어요";
        }

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

    /// <summary>
    /// 샤워하는 이미지 오브젝트를 활성화
    /// </summary>
    /// <returns></returns>
    IEnumerator TakeAShowerAnimation()
    {
        shower.SetActive(true);
        yield return new WaitForSeconds(3f);
        shower.SetActive(false);
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
            string[] thinkingText = new string[3];
            thinkingText[0] = "왜..\n또 씻지?";
            thinkingText[1] = "지도.. 씻고\n또 씻어라";
            thinkingText[2] = "아 저\n깨끗하다고요";

            bubbleText = thinkingText[Random.Range(0, 3)];
        }

        // 공부했을 때
        if (st == "Smart")
        {
            string[] thinkingText = new string[2];
            thinkingText[0] = "공부 다 했는데.";
            thinkingText[1] = "지는.. \n공부 안하면서..";

            bubbleText = thinkingText[Random.Range(0, 2)];
        }


        bubble.SetActive(true);

        bubbleImg.sprite = bubbleSprites[0];
        bubbleRT.anchoredPosition = new Vector3(Random.Range(-310, 260), Random.Range(-170, 100), 0);

        bubbleTMPro.text = bubbleText;

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
            string[] thinkingText = new string[3];
            thinkingText[0] = "히히\n깨끗해";
            thinkingText[1] = "기분 조아\n히히";
            thinkingText[2] = "나는\n뽀송해~!";

            bubbleText = thinkingText[Random.Range(0, 3)];
        }

        // 공부했을 때
        if (st == "Smart")
        {
            string[] thinkingText = new string[3];
            thinkingText[0] = "똑똑해지는 기분\n너무 좋아!";
            thinkingText[1] = "난 똑똑해!\n난 멋져!";
            thinkingText[2] = "내가 다 읽어야지~ 히히";

            bubbleText = thinkingText[Random.Range(0, 3)];
        }


        bubble.SetActive(true);

        bubbleImg.sprite = bubbleSprites[1];
        bubbleRT.anchoredPosition = new Vector3(Random.Range(-310, 260), Random.Range(-170, 100), 0);

        bubbleTMPro.text = bubbleText;

        yield return new WaitForSeconds(3f);
        bubble.SetActive(false);
    }

}
