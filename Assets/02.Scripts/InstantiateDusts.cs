using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� �����ϴ� ��ũ��Ʈ
public class InstantiateDusts : MonoBehaviour
{
    public GameObject[] dust;        // ���� ������Ʈ���� ���� ����

    // ���� ������Ʈ�� ���� ��ġ, ������ ũ��� �� �� �ֵ��� ������ ����
    private float dustXMin = -300f;
    private float dustXMax = 80f;

    private float dustYMin = -120f;
    private float dustYMax = 250f;

    private float dustScaleMin = 3;
    private float dustScaleMax = 6;

    float time;

    private void Update()
    {
        if (StatusBar.instance.curClean < 100 && StatusBar.instance.curClean % 10 == 0)
        {
            time += Time.deltaTime;

            if (time > 5)
            {
                time = 0;
                Status.instance.dustCnt++;

                if (Status.instance.dustCnt >= 9)
                {
                    Status.instance.dustCnt = 9; // ������ ������ 9�� �ֱ� ������, IndexError�� ������ ���� ���� ���� timeCnt�� 9�� ������Ų��
                    return;
                }

                InstantiateDust(Status.instance.dustCnt);

            }
        }
    }

    // ������ �����ϴ� �޼���
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
