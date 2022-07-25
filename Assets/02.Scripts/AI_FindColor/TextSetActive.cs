using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSetActive : MonoBehaviour
{
    public GameObject descriptivePanel; // AI 씬을 설명하는 캔버스 패널
    public GameObject colorDesText;     // 색상 설명 텍스트
    public GameObject findColor;        // AI 모델이 있는 게임 오브젝트

    void Start()
    {
        descriptivePanel.SetActive(true);
        colorDesText.SetActive(false);
        findColor.SetActive(false);
    }
    
    public void PlayAIFindColor()
    {
        StartCoroutine(DesText());
    }

    IEnumerator DesText()
    {
        descriptivePanel.SetActive(false);
        colorDesText.SetActive(true);
        yield return new WaitForSeconds(5f);
        colorDesText.SetActive(false);
        findColor.SetActive(true);
    }

    public void Skip()
    {
        colorDesText.SetActive(false);
        findColor.SetActive(true);
    }
}
