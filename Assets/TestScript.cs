using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject jelly;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(jelly.GetComponent<MeshRenderer>().material.name);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
