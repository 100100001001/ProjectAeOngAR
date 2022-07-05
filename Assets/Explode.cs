using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject explosion;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Jelly")
        {
            collision.transform.gameObject.transform.position = Vector3.zero;
            //Destroy(collision.transform.gameObject); // destroy jelly
            Instantiate(explosion, collision.transform.position, collision.transform.rotation); 
        }
    }
}

