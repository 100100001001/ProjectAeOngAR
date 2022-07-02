using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ð��� �帧�� ���� ���¸� ��ȭ�ϱ� ���� ��ũ��Ʈ
public class TimeFlowTest : MonoBehaviour
{
    // ���� ����
    public GameObject[] dust;        // ���� ������Ʈ���� ���� ����

    private float dustXMin = -230f;  // ���� ������Ʈ�� ���� ��ġ, ������ ũ��� �� �� �ֵ��� ������ ����
    private float dustXMax = 230f;

    private float dustYMin = -400f;
    private float dustYMax = 280f;

    private float dustScaleMin = 5;
    private float dustScaleMax = 10;

    public int timeCnt = 0;          // �ð��� �帧�� ���� ������ �ϳ��� ������Ű�� ���� �ð� �帧�� ī��Ʈ �ϴ� ����
    
    
    public float time;               // �ð��� �帧�� üũ�ϴ� ����


    void Update()
    {
        time += Time.deltaTime;     // ������ �����ӿ��� ���� �����ӱ����� �ʸ� ���Ͽ� �ð��� �帧 üũ

        if (time > 2)               // ���� ��ȭ �ֱ�
        {
            timeCnt++;
            if (timeCnt >= 9) timeCnt = 9; // ������ ������ 9�� �ֱ� ������, IndexError�� ������ ���� ���� ���� timeCnt�� 9�� ������Ų��

            StatusDecrease(5);
            time = 0;
            InstantiateDust(timeCnt);
            

        }
    }


    // �ð��� �帧�� ���� ���� ��ȭ (Value ����)
    void StatusDecrease(int n)
    {
        StatusBar.instance.HungerValue(false, n);
        StatusBar.instance.CleanValue(false, n);
        StatusBar.instance.SmartValue(false, n);
        StatusBar.instance.ActiveValue(false, n);
        //StatusBar.instance.EnergyValue(false, n);
        StatusBar.instance.HappyValue(false, n);

    }

    // ���� �����ϴ� �޼ҵ�
    void InstantiateDust(int step)
    {
        for (int i = 0; i < step; i++)
        {
            float dustScale = Random.Range(dustScaleMin, dustScaleMax); // ������ ������ ���� ��� ���� ����

            dust[i].GetComponent<RectTransform>().localScale = new Vector2(dustScale, dustScale);
            dust[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(dustXMin, dustXMax), Random.Range(dustYMin, dustYMax));

            dust[i].SetActive(true);

            StatusBar.instance.CleanValue(false, 10);
        }
    }
}
