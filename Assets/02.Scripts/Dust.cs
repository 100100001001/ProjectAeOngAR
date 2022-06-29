using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ð��� ������ ���� ȭ�鿡 ���� ����
public class Dust : MonoBehaviour
{
    public GameObject[] dust; // ���� ������Ʈ���� �޾���

    private float dustXMin = -110f;
    private float dustXMax = 110f;

    private float dustYMin = -180f;
    private float dustYMax = 150f;

    private float dustScaleMin = 2;
    private float dustScaleMax = 5;

    private int dustLen = 2;


    void Start()
    {
        // ���� ���� �� ������ ���� �������� �ð��� ���ؼ�, ���� ������ �ʾ��� �� ���� ����
        string lastTime = PlayerPrefs.GetString("SaveLastTime");
        System.DateTime lastDateTime;
        System.DateTime.TryParse(lastTime, out lastDateTime);
        System.TimeSpan conpareTime = System.DateTime.Now - lastDateTime;

        Debug.Log("���� �ð� : " + System.DateTime.Now.ToString());
        Debug.LogFormat("���� ���� ��, {0}�� �������ϴ�.", conpareTime.TotalSeconds);

        onDestroy();


        #region ���� ����
        if (conpareTime.TotalSeconds > 600) // 10��
        {
            InstantiateDust(1);
            StatusBar.instance.CleanValue(false, 10);
        }

        else if (conpareTime.TotalSeconds > 900) // 15��
        {
            InstantiateDust(2);
            StatusBar.instance.CleanValue(false, 20);

        }

        else if (conpareTime.TotalSeconds > 1200) // 20��
        {
            InstantiateDust(3);
            StatusBar.instance.CleanValue(false, 30);

        }

        else if (conpareTime.TotalSeconds > 1500) // 25��
        {
            InstantiateDust(4);
            StatusBar.instance.CleanValue(false, 40);
        }

        else if (conpareTime.TotalSeconds > 1800) // 30��
        {

            InstantiateDust(5);
            StatusBar.instance.CleanValue(false, 50);
        }

        #endregion
    }

    private void onDestroy()
    {
        PlayerPrefs.SetString("SaveLastTime", System.DateTime.Now.ToString());
        Debug.Log("���� �ð� : " + System.DateTime.Now.ToString());
    }

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
