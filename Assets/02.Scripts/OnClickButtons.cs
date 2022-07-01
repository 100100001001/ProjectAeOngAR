using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 하단 버튼을 눌렀을 때 상태 변화
public class OnClickButtons : MonoBehaviour
{
    public GameObject bubble;            // 말풍선 오브젝트를 담아두는 변수
    private RectTransform bubbleRT;      // 말풍선 위치가 랜덤으로 뜨기 위해 RectTransform을 받아오는 변수
    private Image bubbleImg;             // 말풍선 오브젝트의 Image를 불러오기 위한 변수

    public Sprite[] bubbleSprites;       // 말풍선 sprite들을 담아두는 변수

    private TextMeshProUGUI bubbleTMPro; // TextMeshPro를 담아두기 위한 변수
    private string bubbleText;           // TextMeshPro의 텍스트를 변경하기 위한 변수




    private void Start()
    {
        bubbleRT = bubble.GetComponent<RectTransform>();
        bubbleImg = bubble.GetComponent<Image>();
        

        
    }

    public void TakeShower()
    {
        if (StatusBar.instance.curClean >= 100)      // Clean이 가득 찬 상태인데 샤워 버튼을 눌렀을 때
        {
            StartCoroutine(ThinkingBubble("Clean"));
            StatusBar.instance.HappyValue(false, 10); // 기분이 안 좋아짐
            return;
        }

        StartCoroutine(RecBubble("Clean"));

        StatusBar.instance.CleanValue(true, 50);
        StatusBar.instance.HappyValue(true, 10);

        Status.instance.RemoveDust();                 // 화면에 띄워진 먼지 제거

    }


    // 생각하는 말풍선
    IEnumerator ThinkingBubble(string st)
    {
        // 샤워했을 때
        if (st == "Clean") {
            string[] thinkingTextShower = new string[3];
            thinkingTextShower[0] = "왜..\n또 씻지?";
            thinkingTextShower[1] = "지도.. 씻고\n또 씻어라";
            thinkingTextShower[2] = "아 저\n깨끗하다고요";

            bubbleText = thinkingTextShower[Random.Range(0, 3)];
        }


        bubble.SetActive(true);

        bubbleImg.sprite = bubbleSprites[0];
        bubbleRT.anchoredPosition = new Vector3(Random.Range(-200, 200), Random.Range(-380, 280), 0);

        bubble.GetComponentInChildren<TextMeshProUGUI>().text = bubbleText;

        yield return new WaitForSeconds(2f);
        bubble.SetActive(false);
    }


    // 말하는 말풍선
    IEnumerator RecBubble(string st)
    {
        // 샤워했을 때
        if (st == "Clean") {
            string[] thinkingTextShower = new string[3];
            thinkingTextShower[0] = "히히\n깨끗해";
            thinkingTextShower[1] = "기분 조아\n히히";
            thinkingTextShower[2] = "나는\n뽀송해~!";

            bubbleText = thinkingTextShower[Random.Range(0, 3)];
        }


        bubble.SetActive(true);

        bubbleImg.sprite = bubbleSprites[1];
        bubbleRT.anchoredPosition = new Vector3(Random.Range(-200, 200), Random.Range(-380, 280), 0);

        bubble.GetComponentInChildren<TextMeshProUGUI>().text = bubbleText;

        yield return new WaitForSeconds(3f);
        bubble.SetActive(false);
    }
}
