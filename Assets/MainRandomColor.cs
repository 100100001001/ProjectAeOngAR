using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainRandomColor : MonoBehaviour
{
    float time;

    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value);
    }

    private void Update()
    {
        time += Time.deltaTime;
        int n = Random.Range(3, 10);

        if (time >= n)
        {
            gameObject.GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value);
            time = 0;
        }
    }
}
