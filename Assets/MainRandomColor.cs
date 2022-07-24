using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainRandomColor : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value);
    }
}
