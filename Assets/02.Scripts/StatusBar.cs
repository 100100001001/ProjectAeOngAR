using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//// ����� ���� �ڷ���
//[System.Serializable] // ����ȭ
//public class StatusValue
//{

//    public enum StatusType { HUNGER, CLEAN, SMART, ACTIVE, ENERGY, HAPPY }

//    public StatusType type;
//    public Slider bar;
//    public float maxValue = 100;
//    public float curValue;


//    //// ������ -> �ʱⰪ
//    //public StatusValue(float _maxValue, float _curValue)
//    //{
//    //    maxValue = _maxValue;
//    //    curValue = _curValue;
//    //}
//}


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


    #region ���¹�

    public Slider hungerBar;

    private float maxHunger = 100;
    private float curHunger = 50;


    public Slider cleanBar;

    private float maxClean = 100;
    private float curClean = 50;


    public Slider smartBar;

    private float maxSmart = 100;
    private float curSmart = 0;


    public Slider activeBar;

    private float maxActive = 100;
    private float curActive = 0;


    public Slider energyBar;

    private float maxEnergy = 100;
    private float curEnergy = 100;


    public Slider happyBar;

    private float maxHappy = 100;
    private float curHappy = 0;

    #endregion

    //public StatusValue statusValue;



    void Start()
    {
        HandleStatusBar(); // �ʱ�ȭ
        //statusValue.bar.value = (float)statusValue.curValue / (float)statusValue.maxValue; // �ʱ�ȭ
    }

    void Update()
    {
        HandleStatusBar();
    }

    // ���� �ʱ�ȭ
    public void HandleStatusBar()
    {
        hungerBar.value = (float)curHunger / (float)maxHunger;
        cleanBar.value = (float)curClean / (float)maxClean;
        smartBar.value = (float)curSmart / (float)maxSmart;
        activeBar.value = (float)curActive / (float)maxActive;
        energyBar.value = (float)curEnergy / (float)maxEnergy;
        happyBar.value = (float)curHappy / (float)maxHappy;
        //statusValue.bar.value = (float)statusValue.curValue / (float)statusValue.maxValue; // �ʱ�ȭ
    }



    #region ���¹� ���� �޼ҵ�

    public void HungerValue(bool val, int n)
    {
        if (val) curHunger += n;
        else curHunger -= n;
    }
    public void CleanValue(bool val, int n)
    {
        if (val) curClean += n;
        else curClean -= n;
    }
    public void SmartValue(bool val, int n)
    {
        if (val) curSmart += n;
        else curSmart -= n;
    }
    public void ActiveValue(bool val, int n)
    {
        if (val) curActive += n;
        else curActive -= n;
    }
    public void EnergyValue(bool val, int n)
    {
        if (val) curEnergy += n;
        else curEnergy -= n;
    }
    public void HappyValue(bool val, int n)
    {
        if (val) curHappy += n;
        else curHappy -= n;
    }

    #endregion




}
