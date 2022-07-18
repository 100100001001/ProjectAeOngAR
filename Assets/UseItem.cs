using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    public Slider slider;


    float curTime = 10f; // �����̴��� Value���� �������ֱ� ���� �ð� ����
    float maxTime = 10f; // �����̴� Value�� �ִ밪

    bool stopTimer = false;
   
    
    void Update()
    {
        if (stopTimer == false)
        {
            curTime -= Time.deltaTime;
            slider.value = (float)curTime / (float)maxTime; // �����̴��� Value�� ���
        }

        if (curTime <= 0 && stopTimer == false)  
        {
            stopTimer = true;
            curTime = 10f;
        }
    }

    void Use()
    {
        StatusBar.instance.HappyValue(true, 20);
        StatusBar.instance.HungerValue(true, 20);

        stopTimer = false;
    }




}
