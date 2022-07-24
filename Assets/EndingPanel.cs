using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPanel : MonoBehaviour
{
    float time;

    [SerializeField]
    float colorA = 0f;

    bool ending;

    void Start()
    {
        ending = true;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (ending && time > 0.2)
        {
            if (colorA > 255) ending = false;

            time = 0;
            colorA++;
            Color color = gameObject.GetComponent<Image>().color;
            color.a = colorA;
            Debug.Log(colorA);
        }
    }



}
