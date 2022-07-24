using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSetActive : MonoBehaviour
{
    public TextMeshProUGUI descriptiveText; // 설명 텍스트
    public GameObject colorDesText;
    public GameObject FindColor;

    public GameObject BackButton;

    void Start()
    {
        colorDesText.SetActive(false);
        FindColor.SetActive(false);


        StartCoroutine(DesText());
    }

    IEnumerator DesText()
    {
        colorDesText.SetActive(true);
        yield return new WaitForSeconds(10f);

        colorDesText.SetActive(false);
        FindColor.SetActive(true);
    }

    public void Skip()
    {
        colorDesText.SetActive(false);
        FindColor.SetActive(true);
    }
}
