using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyIncreaseDueToSleep : MonoBehaviour
{
    int n = 5;
    float time;

    void Update()
    {
        time += Time.deltaTime;

        if (time > 2)
        {
            StatusBar.instance.EnergyValue(true, n);
            time = 0;
        }
    }
}
