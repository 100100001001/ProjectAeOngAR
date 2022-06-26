using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayer : MonoBehaviour
{
    public Sprite[] playerSprites;
    private Image mySprite;


    void Start()
    {
        mySprite = GetComponent<Image>();
        Evo(Status.instance.evo);
    }

    void Update()
    {
        Evo(Status.instance.evo);
    }

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
                //case Status.Evolution.CHILD:
                //    mySprite = playerSprites[2];
                //    return;
                //case Status.Evolution.YOUTH:
                //    mySprite = playerSprites[3];
                //    return;

        }
    }
}
