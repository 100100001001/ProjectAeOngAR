using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    // �̱��� ���ٿ� ������Ƽ
    public static StatusBar instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�Ƽ� �Ҵ�
                m_instance = FindObjectOfType<StatusBar>();
            }
            // �̱��� ������Ʈ ��ȯ
            return m_instance;
        }
    }
    private static StatusBar m_instance; // �̱����� �Ҵ�� static ����


    public Slider happyBar;

    private float maxHappy = 100;
    private float curHappy = 0;



    void Start()
    {
        happyBar.value = (float)curHappy / (float)maxHappy; // �ʱ�ȭ
    }

    void Update()
    {
        HandleStatusBar();
    }

    private void HandleStatusBar()
    {
        happyBar.value = (float)curHappy / (float)maxHappy; // �ʱ�ȭ
    }



    public void HappyIncrease()
    {
        curHappy += 2;
    }
    public void HappyDecrease()
    {
        curHappy -= 2;
    }
}
