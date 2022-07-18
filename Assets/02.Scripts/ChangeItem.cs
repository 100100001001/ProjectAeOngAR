using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 화살표를 눌렀을 때 플레이어(캐릭터)창을 바꾸는 스크립트
public class ChangeItem : MonoBehaviour
{
    public GameObject[] Arrows;       // 화살표를 껐다켰다하기 위해 화살표들을 받는 변수
    public GameObject itemParents;
    private int count = 0;            // 플레이어(캐릭터)창 배열의 인덱스로 접근하기 위해 만든 변수

    List<GameObject> itemlst = new List<GameObject>();

    int itemlstCnt = 0;



    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDesText;


    private void Update()
    {


        if (itemlst.Count == 0)
        {
            Arrows[0].SetActive(false);
            Arrows[1].SetActive(false);

            itemNameText.text = "아이템이 없어요!";
            itemDesText.text = "또바기와 미니 게임을 해보세요.\n또바기에게 선물을 줄 수 있을지도?";

            return;
        }


        else if (itemlst.Count > 0)
        {
            // 플레이어(캐릭터)창의 활성화 여부에 따라 화살표도 활성화/비활성화 해줌
            if (itemlst[0].activeSelf) Arrows[0].SetActive(false);
            else if (itemlst[itemlst.Count-1].activeSelf) Arrows[1].SetActive(false);
            else
            {
                Arrows[0].SetActive(true);
                Arrows[1].SetActive(true);
            }


        }


    }

    // 오른쪽 화살표를 눌렀을 때
    public void Right()
    {
        if (count+1 >= itemlst.Count) return;

        itemlst[count].SetActive(false);
        count++;
        itemlst[count].SetActive(true);
    }

    // 왼쪽 화살표를 눌렀을 때
    public void Left()
    {
        if (count-1 < 0) return;

        itemlst[count].SetActive(false);
        count--;
        itemlst[count].SetActive(true);
    }
}
