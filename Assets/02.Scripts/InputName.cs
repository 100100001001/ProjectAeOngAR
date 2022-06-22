using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputName : MonoBehaviour
{
    public TMP_InputField playerNameInput; // 플레이어(캐릭터) 이름을 받음
    public TextMeshProUGUI tmpName;        // 플레이어의 위에 띄워 질 이름
    private string playerName;             // TMP_InputField 로 받은 text를 TextMeshProUGUI 에 적용하기 위해 string타입의 변수 생성


    public TextAsset itemDB;               // 이름을 지정하지 않았을 때 랜덤으로 지정된 기본 이름

    private List<string> names = new List<string>();

    void Start()
    {
        // enter(\n)를 기준으로 잘라준다 -> 배열 생성 
        string[] lines = itemDB.text.Split('\n');
        // Debug.Log(lines[0]);
        foreach (var line in lines)
        {
            // 탭(\t) 기준으로 잘라준다
            string[] rows = line.Split('\t');
            names.Add(rows[0]);
        }
        
        playerName = names[Random.Range(0, names.Count)];
        tmpName.GetComponent<TextMeshProUGUI>().text = playerName;
        Debug.Log(playerName);
    }

    public void InputButtonClick()
    {
        playerName = playerNameInput.GetComponent<TMP_InputField>().text;
        tmpName.GetComponent<TextMeshProUGUI>().text = playerName;
    }
}
