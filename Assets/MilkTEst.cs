using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkTEst : MonoBehaviour
{
    float time;
    public Texture[] tx;

    void Update()
    {
        time += Time.deltaTime;

        if (time > 3)
        {
            time = 0;
            int n = Random.Range(0, tx.Length);
            gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", tx[n]);

        }

    }
}
