using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 먼지들을 생성하는 스크립트
public class InstantiateDusts : MonoBehaviour
{
    public GameObject[] dust;        // 먼지 오브젝트들을 담을 변수

    //// 먼지 오브젝트가 랜덤 위치, 랜덤한 크기로 뜰 수 있도록 범위값 지정
    // 먼지의 X값 위치 최솟값, 최댓값
    private float dustXMin = -350f;
    private float dustXMax = 255f;

    // 먼지의 Y값 위치 최솟값, 최댓값
    private float dustYMin = -175f;
    private float dustYMax = 175f;

    // 먼지의 크기 최솟값, 최댓값
    private float dustScaleMin = 3;
    private float dustScaleMax = 6;

    float time; // 시간의 흐름에 따라 생성되는 먼지가 증가되어야 하기 때문에, 시간을 잴 변수 선언

    private void Update()
    {
        // curClean값이 100이 아니고, curClean값을 10으로 나눴을 때 나머지없이 딱 떨어지는 경우
        if (StatusBar.instance.curClean < 100 && StatusBar.instance.curClean % 10 == 0) 
        {
            time += Time.deltaTime;

            if (time > 60)
            {
                time = 0; // time 초기화
                Status.instance.dustCnt++;

                if (Status.instance.dustCnt >= 9)
                {
                    Status.instance.dustCnt = 9; // 증가할 먼지가 9개 있기 때문에, IndexError가 나오는 것을 막기 위해 timeCnt를 9에 고정시킨다
                    return;
                }

                InstantiateDust(Status.instance.dustCnt);

            }
        }
    }

    // 먼지를 생성하는 메서드
    void InstantiateDust(int step)
    {
        for (int i = 0; i < step; i++)
        {
            float dustScale = Random.Range(dustScaleMin, dustScaleMax); // 랜덤한 스케일 값을 얻기 위한 변수

            // 먼지의 위치와 스케일 값 변경
            dust[i].GetComponent<RectTransform>().localScale = new Vector2(dustScale, dustScale);
            dust[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(dustXMin, dustXMax), Random.Range(dustYMin, dustYMax));

            dust[i].SetActive(true);
        }
    }
}
