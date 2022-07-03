using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� �����ϴ� ��ũ��Ʈ
public class InstantiateDusts : MonoBehaviour
{
    public GameObject[] dust;        // ���� ������Ʈ���� ���� ����

    private float dustXMin = -220f;  // ���� ������Ʈ�� ���� ��ġ, ������ ũ��� �� �� �ֵ��� ������ ����
    private float dustXMax = 220f;

    private float dustYMin = -450f;
    private float dustYMax = 300f;

    private float dustScaleMin = 5;
    private float dustScaleMax = 10;

    float time;

    private void Update()
    {
        if (StatusBar.instance.curClean < 100 && StatusBar.instance.curClean % 10 == 0)
        {
            time += Time.deltaTime;

            if (time > 5)
            {
                if (Status.instance.dustCnt >= 9) Status.instance.dustCnt = 9; // ������ ������ 9�� �ֱ� ������, IndexError�� ������ ���� ���� ���� timeCnt�� 9�� ������Ų��

                Status.instance.dustCnt++;
                InstantiateDust(Status.instance.dustCnt);

                time = 0;
            }
        }
    }

    void InstantiateDust(int step)
    {
        for (int i = 0; i < step; i++)
        {
            float dustScale = Random.Range(dustScaleMin, dustScaleMax); // ������ ������ ���� ��� ���� ����

            dust[i].GetComponent<RectTransform>().localScale = new Vector2(dustScale, dustScale);
            dust[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(dustXMin, dustXMax), Random.Range(dustYMin, dustYMax));

            dust[i].SetActive(true);
        }
    }
}
