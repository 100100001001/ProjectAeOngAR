using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    public Slider slider;


    float curTime = 10f; // 슬라이더의 Value값을 조정해주기 위한 시간 변수
    float maxTime = 10f; // 슬라이더 Value의 최대값

    bool stopTimer = true;

    void Update()
    {
        if (stopTimer == false)
        {
            curTime -= Time.deltaTime;
            slider.value = (float)curTime / (float)maxTime; // 슬라이더의 Value를 계산
            gameObject.GetComponent<Button>().interactable = false;
        }

        if (curTime <= 0 && stopTimer == false)  
        {
            stopTimer = true;
            curTime = 10f;
            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void Use()
    {
        StatusBar.instance.HappyValue(true, 20);
        StatusBar.instance.HungerValue(true, 20);

        stopTimer = false;
    }




}
