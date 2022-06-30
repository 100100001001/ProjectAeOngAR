using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnClickButtons : MonoBehaviour
{
    public GameObject bubbleThinking;
    private RectTransform bubbleThinkingRT;
    private string thinkingText;

    public GameObject bubbleRec;
    private RectTransform bubbleRecRT;



    private void Start()
    {
        bubbleThinkingRT = bubbleThinking.GetComponent<RectTransform>();
        bubbleRecRT = bubbleRec.GetComponent<RectTransform>();



        bubbleThinkingRT.anchoredPosition = new Vector3(Random.Range(-200, 200), Random.Range(-380, 280), 0);
        bubbleThinking.SetActive(true);
        StatusBar.instance.HappyValue(false, 10);
        TakeShower();
    }




    public void TakeShower()
    {
        if (StatusBar.instance.curClean >= 100)
        {
            bubbleThinking.transform.position = new Vector3(Random.Range(-200, 200), Random.Range(-380, 280), 0);
            bubbleThinking.SetActive(true);
            StatusBar.instance.HappyValue(false, 10);
        }

        Status.instance.RemoveDust();

        StatusBar.instance.HappyValue(true, 10);
        StatusBar.instance.CleanValue(true, 50);
    }



    IEnumerator BubbleText()
    {
        //string[] stop = new string[3];
        //stop[0] = "ÀÌÁ¦ ±¦Âú¾Æ";
        //stop[1] = "...";
        //stop[2] = "±×¸¸~";

        //string[] angry = new string[3];
        //stop[0] = "¿Ö ±«·ÓÇô!!!!";
        //stop[1] = "³Ê¹«ÇØ!!!!";
        //stop[2] = "½È´Ù°í!!!!";


        //touchText.GetComponent<Text>().text = touchCnt + " / " + textAfterTouch;
        yield return new WaitForSeconds(1f);
        //touchText.SetActive(false);
    }

}
