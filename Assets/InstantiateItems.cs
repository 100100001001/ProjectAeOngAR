using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateItems : MonoBehaviour
{
    public GameObject items;
    public GameObject milk;
    public GameObject food;

    public Texture[] milkTextures; // ���� �������� ������ �������ֱ� ���� �ؽ�ó���� �޾���


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
