using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//// 사용자 정의 자료형
//[System.Serializable] // 직렬화
//public class StatusValue
//{

//    public enum StatusType { HUNGER, CLEAN, SMART, ACTIVE, ENERGY, HAPPY }

//    public StatusType type;
//    public Slider bar;
//    public float maxValue = 100;
//    public float curValue;


//    //// 생성자 -> 초기값
//    //public StatusValue(float _maxValue, float _curValue)
//    //{
//    //    maxValue = _maxValue;
//    //    curValue = _curValue;
//    //}
//}


public class StatusBar : MonoBehaviour
{
    // 싱글턴 접근용 프로퍼티
    public static StatusBar instance
    {
        get
        {
            // 만약 싱글턴 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아서 할당
                m_instance = FindObjectOfType<StatusBar>();
            }
            // 싱글턴 오브젝트 반환
            return m_instance;
        }
    }
    private static StatusBar m_instance; // 싱글턴이 할당될 static 변수


    #region 상태바

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
        HandleStatusBar(); // 초기화
        //statusValue.bar.value = (float)statusValue.curValue / (float)statusValue.maxValue; // 초기화
    }

    void Update()
    {
        HandleStatusBar();
    }

    // 상태 초기화
    public void HandleStatusBar()
    {
        hungerBar.value = (float)curHunger / (float)maxHunger;
        cleanBar.value = (float)curClean / (float)maxClean;
        smartBar.value = (float)curSmart / (float)maxSmart;
        activeBar.value = (float)curActive / (float)maxActive;
        energyBar.value = (float)curEnergy / (float)maxEnergy;
        happyBar.value = (float)curHappy / (float)maxHappy;
        //statusValue.bar.value = (float)statusValue.curValue / (float)statusValue.maxValue; // 초기화
    }



    #region 상태바 조절 메소드

    public void HungerValue(bool val)
    {
        if (val) curHunger += 2;
        else curHunger -= 2;
    }
    public void CleanValue(bool val)
    {
        if (val) curClean += 2;
        else curClean -= 2;
    }
    public void SmartValue(bool val)
    {
        if (val) curSmart += 2;
        else curSmart -= 2;
    }
    public void ActiveValue(bool val)
    {
        if (val) curActive += 2;
        else curActive -= 2;
    }
    public void EnergyValue(bool val)
    {
        if (val) curEnergy += 2;
        else curEnergy -= 2;
    }
    public void HappyValue(bool val)
    {
        if (val) curHappy += 2;
        else curHappy -= 2;
    }

    #endregion











    //public void ValueIncrease()
    //{
    //    //// 매개변수 StatusValue.StatusType stateName
    //    //switch (stateName)
    //    //{
    //    //    case StatusValue.StatusType.HUNGER:
    //    //        return;
    //    //}
    //    //curHappy += 2;
    //    statusValue.curValue += 2;
    //}

    //public void ValueDecrease()
    //{
    //    //curHappy -= 2;
    //    statusValue.curValue -= 2;
    //}
}
