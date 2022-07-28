using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTest : MonoBehaviour
{
    float time;
    Color color;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if (time > 2)
        {
            color = new Color32(225, (byte)Random.Range(0, 101), (byte)Random.Range(0, 256), 255);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
            Debug.Log(color);
            time = 0;
        }
    }
}
