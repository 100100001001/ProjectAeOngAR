using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public enum StateType { ACTIVE, EMOTION }
    public StateType type;
    public int value;
    public int count;

    public enum Evolution { EGG, BABY, CHILD, YOUTH }
    public Evolution evo = Evolution.EGG;

    // 성별
    public TextMeshProUGUI sText;
    private string sString;

    private void Start()
    {

        if (Random.Range(0, 2) == 0) sString = "여";
        else sString = "남";

        sText.text = sString;
    }

    private void Update()
    {
    }



}
