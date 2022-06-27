using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputName : MonoBehaviour
{
    public TMP_InputField playerNameInput; // 사용자가 직접 수정한 플레이어(캐릭터) 이름
    public TextMeshProUGUI tmpName;        // 플레이어(캐릭터)에게 적용될 이름
    public TextMeshProUGUI tmpNameCharWin; // 플레이어(캐릭터) 창에서 보여질 이름
    private string tmpText;                // TMP_InputField 로 받은 text
    private string playerName;             // TMP_InputField 로 받은 text를 TextMeshProUGUI 에 적용하기 위해 string타입의 변수 생성


    // 기본 이름 설정
    public TextAsset nameDB;                         // 이름을 지정하지 않았을 때 랜덤으로 지정된 기본 이름들 
    private List<string> names = new List<string>(); // nameDB에서 받아온 기본 이름 리스트

    void Start()
    {
        //// 기본 이름 설정

        // enter(\n)를 기준으로 잘라서 배열 생성 
        string[] lines = nameDB.text.Split('\n');
        // Debug.Log(lines[0]);
        foreach (var line in lines)
        {
            // 탭(\t) 기준으로 잘라준다
            string[] rows = line.Split('\t');
            names.Add(rows[0]);
        }

        // 이름 리스트에서 랜덤으로 숫자를 뽑아서 playerName에 할당
        playerName = names[Random.Range(0, names.Count)];
        tmpName.GetComponent<TextMeshProUGUI>().text = playerName;
        tmpNameCharWin.GetComponent<TextMeshProUGUI>().text = playerName;
        //Debug.Log(playerName);
        //Debug.Log(names[1608]);
    }

    // 입력 버튼을 눌렀을 때
    public void InputButtonClick()
    {
        tmpText = playerNameInput.GetComponent<TMP_InputField>().text;

        // tmpText가 비어있지 있지 않을 때만 이름 변경
        if (tmpText.Length > 0)
        {
            Debug.Log(tmpText);
            playerName = tmpText;
            tmpName.GetComponent<TextMeshProUGUI>().text = playerName;
            tmpNameCharWin.GetComponent<TextMeshProUGUI>().text = playerName;
        }

    }
}
