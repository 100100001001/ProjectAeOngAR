using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����� ���� �ڷ���
[System.Serializable] // ����ȭ
public class StatusValue
{

    public enum StatusType { HUNGER, CLEAN, SMART, ACTIVE, ENERGY, HAPPY }

    public StatusType type;
    public Slider bar;
    public float maxValue = 100;
    public float curValue;


    //// ������ -> �ʱⰪ
    //public StatusValue(float _maxValue, float _curValue)
    //{
    //    maxValue = _maxValue;
    //    curValue = _curValue;
    //}
}


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


    //public Slider happyBar;

    //private float maxHappy = 100;
    //private float curHappy = 0;

    public StatusValue statusValue;



    void Start()
    {
        //happyBar.value = (float)curHappy / (float)maxHappy; // �ʱ�ȭ
        statusValue.bar.value = (float)statusValue.curValue / (float)statusValue.maxValue; // �ʱ�ȭ
    }

    void Update()
    {
        HandleStatusBar();
    }

    public void HandleStatusBar()
    {
        //happyBar.value = (float)curHappy / (float)maxHappy; // �ʱ�ȭ
        statusValue.bar.value = (float)statusValue.curValue / (float)statusValue.maxValue; // �ʱ�ȭ
    }


    public void ValueIncrease()
    {
        //// �Ű����� StatusValue.StatusType stateName
        //switch (stateName)
        //{
        //    case StatusValue.StatusType.HUNGER:
        //        return;
        //}
        //curHappy += 2;
        statusValue.curValue += 2;
    }

    public void ValueDecrease()
    {
        //curHappy -= 2;
        statusValue.curValue -= 2;
    }
}
