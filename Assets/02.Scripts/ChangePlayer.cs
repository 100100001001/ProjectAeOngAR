using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Player Panel(ĳ����â)�� ĳ���͸� �����ϴ� ��ũ��Ʈ
public class ChangePlayer : MonoBehaviour
{
    public Sprite[] playerSprites; // ĳ������ �̹������� �迭�� ����
    private Image mySprite;        // ������ �̹���

    public GameObject childSprout; // Child ���¿����� ����
    public GameObject youthLeaf;   // Youth ���¿����� ��

    void Start()
    {
        mySprite = GetComponent<Image>();
        Evo(Status.instance.evo);
    }

    void Update()
    {
        Evo(Status.instance.evo);
    }

    // ��ȭ �ܰ迡 ���� �̹����� �ٲ�
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
                childSprout.SetActive(true);
                youthLeaf.SetActive(false);
                return;
            case Status.Evolution.YOUTH:
                childSprout.SetActive(false);
                youthLeaf.SetActive(true);
                return;

        }
    }
}
