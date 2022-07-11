using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explode : MonoBehaviour
{
    public GameObject explosion;
    public bool addScore = false;

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Jelly")
        {
            //collision.transform.gameObject.transform.position = Vector3.zero;
            StartCoroutine(SetActiveJelly(collision.transform.gameObject));

            Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            addScore = true;
        }
    }


    IEnumerator SetActiveJelly(GameObject ob)
    {
        ob.SetActive(false);
        yield return new WaitForSeconds(3f);
        ob.SetActive(true);


    }
}

