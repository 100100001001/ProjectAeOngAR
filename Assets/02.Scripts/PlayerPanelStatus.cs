using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 플레이어(캐릭터)창의 Status값을 적용하는 스크립트
public class PlayerPanelStatus : MonoBehaviour
{
    private TextMeshProUGUI[] panelStatusTMP;


    void Start()
    {
        panelStatusTMP = GetComponentsInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        panelStatusTMP[0].text = "배부름 : " + (int)StatusBar.instance.curHunger + "%";
        panelStatusTMP[1].text = "깨끗함 : " + (int)StatusBar.instance.curClean + "%";
        panelStatusTMP[2].text = "똑똑함 : " + (int)StatusBar.instance.curSmart + "%";
        panelStatusTMP[3].text = "움직임 : " + (int)StatusBar.instance.curActive + "%";
        panelStatusTMP[4].text = "기력 : " + (int)StatusBar.instance.curEnergy + "%";
        panelStatusTMP[5].text = "행복 : " + (int)StatusBar.instance.curHappy + "%";
    }
}
