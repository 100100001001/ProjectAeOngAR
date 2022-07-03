using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 먼지들을 생성하는 스크립트
public class InstantiateDusts : MonoBehaviour
{
    public GameObject[] dust;        // 먼지 오브젝트들을 담을 변수

    private float dustXMin = -220f;  // 먼지 오브젝트가 랜덤 위치, 랜덤한 크기로 뜰 수 있도록 범위값 지정
    private float dustXMax = 220f;

    private float dustYMin = -450f;
    private float dustYMax = 300f;

    private float dustScaleMin = 5;
    private float dustScaleMax = 10;

    float time;

    private void Update()
    {
        if (StatusBar.instance.curClean < 100 && StatusBar.instance.curClean % 10 == 0)
        {
            time += Time.deltaTime;

            if (time > 5)
            {
                if (Status.instance.dustCnt >= 9) Status.instance.dustCnt = 9; // 증가할 먼지가 9개 있기 때문에, IndexError가 나오는 것을 막기 위해 timeCnt를 9에 고정시킨다

                Status.instance.dustCnt++;
                InstantiateDust(Status.instance.dustCnt);

                time = 0;
            }
        }
    }

    void InstantiateDust(int step)
    {
        for (int i = 0; i < step; i++)
        {
            float dustScale = Random.Range(dustScaleMin, dustScaleMax); // 랜덤한 스케일 값을 얻기 위한 변수

            dust[i].GetComponent<RectTransform>().localScale = new Vector2(dustScale, dustScale);
            dust[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(dustXMin, dustXMax), Random.Range(dustYMin, dustYMax));

            dust[i].SetActive(true);
        }
    }
}
