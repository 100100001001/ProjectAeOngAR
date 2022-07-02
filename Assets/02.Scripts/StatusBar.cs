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

    public float maxHunger = 100;
    public float curHunger = 50;


    public Slider cleanBar;

    public float maxClean = 100;
    public float curClean = 50;


    public Slider smartBar;

    public float maxSmart = 100;
    public float curSmart = 0;


    public Slider activeBar;

    public float maxActive = 100;
    public float curActive = 0;


    public Slider energyBar;

    public float maxEnergy = 100;
    public float curEnergy = 50;


    public Slider happyBar;

    public float maxHappy = 100;
    public float curHappy = 0;

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

        // Debug.Log("hungerBar "+hungerBar.value);
        // Debug.Log("cleanBar "+cleanBar.value);
        // Debug.Log("smartBar "+smartBar.value);
        // Debug.Log("activeBar "+activeBar.value);
        // Debug.Log("energyBar "+energyBar.value);
        //Debug.Log("happyBar "+happyBar.value);
        //Debug.Log("curHappy "+curHappy);
        //Debug.Log("maxHappy "+maxHappy);
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



    #region 상태(바) 조절 메소드

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
