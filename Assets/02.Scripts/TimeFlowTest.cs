using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 시간의 흐름에 따라 상태를 변화하기 위한 스크립트
public class TimeFlowTest : MonoBehaviour
{
    public float time;               // 시간의 흐름을 체크하는 변수


    void Update()
    {
        time += Time.deltaTime;     // 마지막 프레임에서 현재 프레임까지의 초를 더하여 시간의 흐름 체크

        if (time > 5)               // 상태 변화 주기
        {

            StatusDecrease(5);
            time = 0;
            

        }
    }


    // 시간의 흐름에 따른 상태 변화 (Value 감소)
    void StatusDecrease(int n)
    {
        StatusBar.instance.HungerValue(false, n);
        StatusBar.instance.CleanValue(false, n);
        StatusBar.instance.SmartValue(false, n);
        StatusBar.instance.ActiveValue(false, n);
        StatusBar.instance.EnergyValue(false, n);
        StatusBar.instance.HappyValue(false, n);

    }

}
