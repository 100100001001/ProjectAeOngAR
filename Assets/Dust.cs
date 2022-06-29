using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ð��� ������ ���� ȭ�鿡 ���� ����
public class Dust : MonoBehaviour
{
    public GameObject[] dust; // ���� ������Ʈ

    private float dustXMin = -90f;
    private float dustXMax = 90f;
    private float dustYMin = -120f;
    private float dustYMax = 120f;
    private int dustLen = 2;



    void Start()
    {
        string lastTime = PlayerPrefs.GetString("SaveLastTime");
        System.DateTime lastDateTime = System.DateTime.Parse(lastTime);
        System.TimeSpan conpareTime = System.DateTime.Now - lastDateTime;

        Debug.Log("���� �ð� : " + System.DateTime.Now.ToString());
        Debug.LogFormat("���� ���� ��, {0}�� �������ϴ�.", conpareTime.TotalSeconds);

        onDestroy();



        if (conpareTime.TotalSeconds > 1)
        {
            InstantiateDust(2);
        }

        else if (conpareTime.TotalSeconds > 5)
        {
            InstantiateDust(4);
        }

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

                dust[i].transform.localScale = new Vector3(5, 5, 5);
                Instantiate(dust[i], new Vector3(dustXPos, dustYPos, 500), Quaternion.identity);
            }
        }
    }


}
