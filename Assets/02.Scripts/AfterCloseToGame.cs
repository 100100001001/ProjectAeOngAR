using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 시간이 지남에 따라 상태 변경
public class AfterCloseToGame : MonoBehaviour
{
    // 먼지 생성
    public GameObject[] dust;

    private float dustXMin = -110f;
    private float dustXMax = 110f;

    private float dustYMin = -180f;
    private float dustYMax = 150f;

    private float dustScaleMin = 2;
    private float dustScaleMax = 5;

    private int dustLen = 2;


    public GameObject ttest;
    public TextMeshProUGUI t;


    void Start()
    {
        t = ttest.GetComponent<TextMeshProUGUI>();


        // 어플 종료 후 다음에 켜질 때까지의 시간을 구함
        string lastTime = PlayerPrefs.GetString("SaveLastTime");
        System.DateTime lastDateTime;
        System.DateTime.TryParse(lastTime, out lastDateTime);
        System.TimeSpan conpareTime = System.DateTime.Now - lastDateTime;

        Debug.Log("실행 시간 : " + System.DateTime.Now.ToString());
        Debug.LogFormat("게임 종료 후, {0}초 지났습니다.", conpareTime.TotalSeconds);

        onDestroy();


        #region 먼지 생성

        if (conpareTime.TotalSeconds > 1)
        {
            //InstantiateDust(3);
            StatusBar.instance.CleanValue(false, 30);

            // StatusBar.instance.HungerValue(false, 30);
            // StatusBar.instance.SmartValue(false, 30);
            // StatusBar.instance.ActiveValue(false, 30);
            // StatusBar.instance.EnergyValue(false, 30);
            // StatusBar.instance.HappyValue(false, 30);
        }


        //if (conpareTime.TotalSeconds > 600) // 10분
        //{
        //    InstantiateDust(1);
        //    StatusBar.instance.CleanValue(false, 10);
        //}

        //else if (conpareTime.TotalSeconds > 900) // 15분
        //{
        //    InstantiateDust(2);
        //    StatusBar.instance.CleanValue(false, 20);

        //}

        //else if (conpareTime.TotalSeconds > 1200) // 20분
        //{
        //    InstantiateDust(3);
        //    StatusBar.instance.CleanValue(false, 30);

        //}

        //else if (conpareTime.TotalSeconds > 1500) // 25분
        //{
        //    InstantiateDust(4);
        //    StatusBar.instance.CleanValue(false, 40);
        //}

        //else if (conpareTime.TotalSeconds > 1800) // 30분
        //{

        //    InstantiateDust(5);
        //    StatusBar.instance.CleanValue(false, 50);
        //}

        #endregion
    }

    private void onDestroy()
    {
        PlayerPrefs.SetString("SaveLastTime", System.DateTime.Now.ToString());

        Debug.Log(PlayerPrefs.GetString("SaveLastTime"));

        //t.text = PlayerPrefs.GetString("SaveLastTime");

        if (PlayerPrefs.GetString("SaveLastTime") == null || PlayerPrefs.GetString("SaveLastTime") == "0") return;
        //ttest.SetActive(true);

        Debug.Log("종료 시간 : " + System.DateTime.Now.ToString());
    }




    //------------------------수정해야 함!!!------------------------//
    private void InstantiateDust(int step)
    {
        for (int j = 0; j < step; j++)
        {
            for (int i = 0; i < dustLen; i++)
            {
                float dustXPos = Random.Range(dustXMin, dustXMax);
                float dustYPos = Random.Range(dustYMin, dustYMax);
                float dustScale = Random.Range(dustScaleMin, dustScaleMax);

                dust[i].transform.localScale = new Vector3(dustScale, dustScale, dustScale);

                Instantiate(dust[i], new Vector3(dustXPos, dustYPos, 500), Quaternion.identity);
            }
        }
    }


}
