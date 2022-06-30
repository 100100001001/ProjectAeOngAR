using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Player Panel(캐릭터창)의 캐릭터를 변경하는 스크립트
public class ChangePlayer : MonoBehaviour
{
    public Sprite[] playerSprites; // 캐릭터의 이미지들을 배열로 받음
    private Image mySprite;        // 변경할 이미지

    public GameObject childSprout; // Child 상태에서의 새싹
    public GameObject youthLeaf;   // Youth 상태에서의 잎

    void Start()
    {
        mySprite = GetComponent<Image>();
        Evo(Status.instance.evo);
    }

    void Update()
    {
        Evo(Status.instance.evo);
    }

    // 진화 단계에 따라서 이미지가 바뀜
    public void Evo(Status.Evolution stage)
    {
        switch (stage)
        {
            case Status.Evolution.EGG:
                mySprite.sprite = playerSprites[0];
                return;
            case Status.Evolution.BABY:
                mySprite.sprite = playerSprites[1];
                return;
            case Status.Evolution.CHILD:
                mySprite.sprite = playerSprites[1];
                childSprout.SetActive(true);
                youthLeaf.SetActive(false);
                return;
            case Status.Evolution.YOUTH:
                mySprite.sprite = playerSprites[1];
                childSprout.SetActive(false);
                youthLeaf.SetActive(true);
                return;

        }
    }
}
