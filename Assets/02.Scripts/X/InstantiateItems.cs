using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateItems : MonoBehaviour
{
    public GameObject items;
    public GameObject milk;
    public GameObject food;

    public Texture[] milkTextures; // 우유 아이템의 색상을 지정해주기 위해 텍스처들을 받아줌


    void Update()
    {
        //if (TimerSlider.getMilk)
        //{
        //    milk.GetComponent<Renderer>().material.SetTexture("_MainTex", milkTextures[TimerSlider.milkNumber]);

        //    TimerSlider.getMilk = false;

        //    Instantiate(milk, Vector3.zero, Quaternion.identity).transform.parent = items.transform;

        //}
    }
}
