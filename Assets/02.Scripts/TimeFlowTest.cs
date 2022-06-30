using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeFlowTest : MonoBehaviour
{
    public float time;

    void Start()
    {

    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > 10) {
            StatusDecrease(1);
            time = 0;
        }

    }



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
