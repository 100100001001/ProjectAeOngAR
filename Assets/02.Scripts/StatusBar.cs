using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    public Slider happyBar;

    private float maxHappy = 100;
    private float curHappy = 0;



    void Start()
    {
        happyBar.value = (float)curHappy / (float)maxHappy; // 초기화
    }

    void Update()
    {
        HandleStatusBar();
    }

    private void HandleStatusBar()
    {
        happyBar.value = (float)curHappy / (float)maxHappy; // 초기화
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
