using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 시간의 흐름에 따라 상태를 변화하기 위한 스크립트
public class TimeFlowTest : MonoBehaviour
{
    // 먼지 생성
    public GameObject[] dust;        // 먼지 오브젝트들을 담을 변수

    private float dustXMin = -230f;  // 먼지 오브젝트가 랜덤 위치, 랜덤한 크기로 뜰 수 있도록 범위값 지정
    private float dustXMax = 230f;

    private float dustYMin = -400f;
    private float dustYMax = 280f;

    private float dustScaleMin = 5;
    private float dustScaleMax = 10;

    public int timeCnt = 0;          // 시간의 흐름에 따라 먼지를 하나씩 증가시키기 위해 시간 흐름을 카운트 하는 변수
    
    
    public float time;               // 시간의 흐름을 체크하는 변수


    void Update()
    {
        time += Time.deltaTime;     // 마지막 프레임에서 현재 프레임까지의 초를 더하여 시간의 흐름 체크

        if (time > 2)               // 상태 변화 주기
        {
            timeCnt++;
            if (timeCnt >= 9) timeCnt = 9; // 증가할 먼지가 9개 있기 때문에, IndexError가 나오는 것을 막기 위해 timeCnt를 9에 고정시킨다

            StatusDecrease(5);
            time = 0;
            InstantiateDust(timeCnt);
            

        }
    }


    // 시간의 흐름에 따른 상태 변화 (Value 감소)
    void StatusDecrease(int n)
    {
        StatusBar.instance.HungerValue(false, n);
        StatusBar.instance.CleanValue(false, n);
        StatusBar.instance.SmartValue(false, n);
        StatusBar.instance.ActiveValue(false, n);
        //StatusBar.instance.EnergyValue(false, n);
        StatusBar.instance.HappyValue(false, n);

    }

    // 먼지 생성하는 메소드
    void InstantiateDust(int step)
    {
        for (int i = 0; i < step; i++)
        {
            float dustScale = Random.Range(dustScaleMin, dustScaleMax); // 랜덤한 스케일 값을 얻기 위한 변수

            dust[i].GetComponent<RectTransform>().localScale = new Vector2(dustScale, dustScale);
            dust[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(dustXMin, dustXMax), Random.Range(dustYMin, dustYMax));

            dust[i].SetActive(true);

            StatusBar.instance.CleanValue(false, 10);
        }
    }
}
