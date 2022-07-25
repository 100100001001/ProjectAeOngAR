using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
// 반려동물의 상태를 기록하는 스크립트
public class Status : MonoBehaviour
{
    // 싱글턴 접근용 프로퍼티
    public static Status instance
    {
        get
        {
            // 만약 싱글턴 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아서 할당
                m_instance = FindObjectOfType<Status>();
            }
            // 싱글턴 오브젝트 반환
            return m_instance;
        }
    }
    private static Status m_instance; // 싱글턴이 할당될 static 변수



    //public enum StateType { HUNGER, CLEAN, SMART, ACTIVE, ENERGY, HAPPY } // 캐릭터의 상태를 카운트
    //public StateType type;
    //public int value;
    //public int count;


    // 캐릭터의 상태 변화를 셀 변수 (호감도가 오를 때에만 횟수 증가)
    public int cntClean1;   // 샤워 한 횟수
    public int cntSmart1;   // 공부 한 횟수
    public int cntActive1;  // 활동 한 횟수
    public int cntSleep1;   // 잠 잔 횟수
    public int cntEat1;     // 아이템을 먹은 횟수
    public int cntHappy1;   // 행복한 횟수
    public int cntTouch1;   // 터치 횟수


    public enum Evolution1 { EGG, BABY, CHILD, YOUTH, BREAKEGG } // 캐릭터 성장
    public Evolution1 evo1;                                      // 캐릭터 상태를 담을 변수

    int saveEvo1; // 캐릭터 성장 상태를 저장하는 변수


    // 성별
    //public TextMeshProUGUI sText;
    //private string sString;


    // evo 테스트!!!!!!!!!
    public TextMeshProUGUI evoTestText;


    public int dustCnt = 0;



    private void Start()
    {
        if (ByeButton.bye)
        {
            evo1 = Evolution1.EGG;
            return;
        }


        if (PlayerPrefs.HasKey("E"))
        {
            saveEvo1 = PlayerPrefs.GetInt("E");

            if (saveEvo1 == (int)Evolution1.EGG) evo1 = Evolution1.EGG;
            else if (saveEvo1 == (int)Evolution1.BABY) evo1 = Evolution1.BABY;
            else if (saveEvo1 == (int)Evolution1.CHILD) evo1 = Evolution1.CHILD;
            else if (saveEvo1 == (int)Evolution1.YOUTH) evo1 = Evolution1.YOUTH;
            else if (saveEvo1 == (int)Evolution1.BREAKEGG) evo1 = Evolution1.BREAKEGG;
        }

        // 캐릭터의 처음 상태를 EGG로 지정
        else evo1 = Evolution1.EGG;


        //// 성별 랜덤 지정
        //if (Random.Range(0, 2) == 0) sString = "여";
        //else sString = "남";

        //sText.text = sString;
    }

    private void Update()
    {
        Evo(evo1);

        evoTestText.text = "" + evo1 + "/" + saveEvo1;


        saveEvo1 = (int)evo1;
        PlayerPrefs.SetInt("E", saveEvo1);


        //if (StatusBar.instance.curClean < 50)
        //    AddDust();
    }



    public void Evo(Evolution1 step)
    {
        switch (step)
        {
            case Evolution1.EGG:

                if (cntTouch1 >= 5)
                    if (cntSmart1 >= 1 || cntClean1 >= 1)
                    {
                        evo1 = Evolution1.BREAKEGG;
                        InitCnt();
                    }

                    else if (cntTouch1 >= 6)
                    {
                        evo1 = Evolution1.BREAKEGG;
                        InitCnt();
                    }

                break;

            case Evolution1.BREAKEGG:
                StartCoroutine(BreakEgg());
                return;

            case Evolution1.BABY:
                if (cntTouch1 >= 5)
                    if (cntSmart1 >= 2 || cntClean1 >= 2)
                    {
                        evo1 = Evolution1.CHILD;
                        InitCnt();
                    }


                    else if (cntTouch1 >= 50)
                    {
                        evo1 = Evolution1.CHILD;
                        InitCnt();
                    }

                break;


            case Evolution1.CHILD:
                if (cntTouch1 >= 5)
                    if (cntSmart1 >= 2 || cntClean1 >= 2)
                    {
                        evo1 = Evolution1.YOUTH;
                        InitCnt();
                    }

                    else if (cntTouch1 >= 100)
                    {
                        evo1 = Evolution1.YOUTH;
                        InitCnt();

                    }
                break;

            case Evolution1.YOUTH:
                break;
        }
    }


    IEnumerator BreakEgg()
    {
        yield return new WaitForSeconds(5f);
        evo1 = Evolution1.BABY;
    }


    void InitCnt()
    {
        cntClean1 = 0;
        cntSmart1 = 0;
        cntActive1 = 0;
        cntSleep1 = 0;
        cntEat1 = 0;
        cntHappy1 = 0;
        cntTouch1 = 0;
    }

    //void SaveCnt()
    //{

    //    PlayerPrefs.SetFloat("cntClean1", cntClean1);
    //    PlayerPrefs.SetFloat("cntSmart1", cntSmart1);
    //    PlayerPrefs.SetFloat("cntActive1", cntActive1);
    //    PlayerPrefs.SetFloat("cntSleep1", cntSleep1);
    //    PlayerPrefs.SetFloat("cntEat1", cntEat1);
    //    PlayerPrefs.SetFloat("cntHappy1", cntHappy1);
    //    PlayerPrefs.SetFloat("cntTouch1", cntTouch1);

    //}









    //// 먼지를 활성화하는 메소드
    //public void AddDust()
    //{
    //    switch (evo1)
    //    {
    //        case Evolution1.EGG:
    //            for (int i = 0; i < dustEgg.Length; i++) dustEgg[i].SetActive(true);
    //            break;
    //        case Evolution1.BABY:
    //            for (int i = 0; i < dustBaby.Length; i++) dustBaby[i].SetActive(true);
    //            break;
    //        case Evolution1.CHILD:
    //            for (int i = 0; i < dustChild.Length; i++) dustChild[i].SetActive(true);
    //            break;
    //        case Evolution1.YOUTH:
    //            for (int i = 0; i < dustYouth.Length; i++) dustYouth[i].SetActive(true);
    //            break;
    //    }
    //}

    //// 먼지를 비활성화하는 메소드
    //public void RemoveDust()
    //{

    //    switch (evo1)
    //    {
    //        case Evolution1.EGG:
    //            for (int i = 0; i < dustEgg.Length; i++) dustEgg[i].SetActive(false);
    //            break;
    //        case Evolution1.BABY:
    //            for (int i = 0; i < dustBaby.Length; i++) dustBaby[i].SetActive(false);
    //            break;
    //        case Evolution1.CHILD:
    //            for (int i = 0; i < dustChild.Length; i++) dustChild[i].SetActive(false);
    //            break;
    //        case Evolution1.YOUTH:
    //            for (int i = 0; i < dustYouth.Length; i++) dustYouth[i].SetActive(false);
    //            break;
    //    }
    //}
}
