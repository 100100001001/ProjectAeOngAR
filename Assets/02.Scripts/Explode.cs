using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Explode : MonoBehaviour
{
    public GameObject explosion;
    public GameObject scoreToSpawn;
    public GameObject enemyToSpawn;
    Vector3 killPos;
    Quaternion killRot;
    float waitTime = 3.0f;
    bool bulletCollission = false; // to avoid hittimg multiple dpiders with same bullet
    int jellyScoreNum = 1;




    public Material[] jellyMaterials;
    public TextMeshProUGUI t;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Jelly" && bulletCollission == false)
        {
            //Destroy(collision.transform.gameObject); // destroy spider

            //if (collision.gameObject.GetComponent<MeshRenderer>().material.name == "GreenJelly") jellyScoreNum = 1;
            //else if (collision.gameObject.GetComponent<MeshRenderer>().material.name == "OrangeJelly") jellyScoreNum = 2; 
            //else if (collision.gameObject.GetComponent<MeshRenderer>().material.name == "PurpleJelly") jellyScoreNum = 3; 


            if (collision.transform.GetComponent<MeshRenderer>().material.name == "GreenJelly") Scoring.score += 5;
            else if (collision.transform.GetComponent<MeshRenderer>().material.name == "OrangeJelly") Scoring.score += 10; 
            else if (collision.transform.GetComponent<MeshRenderer>().material.name == "PurpleJelly") Scoring.score += 15;
            else Scoring.score += 2;

            t.text = ""+collision.transform.GetComponent<MeshRenderer>().material.name;

            //Scoring.score += (5 * jellyScoreNum);

            bulletCollission = true;

            //killPos = collision.transform.position;
            //killRot = collision.transform.rotation;
            StartCoroutine(SpawnEnemyAgain(collision.transform.gameObject));

            Quaternion q = new Quaternion(0, -1, 0, 0);
            q *= collision.transform.rotation;

            Destroy(Instantiate(explosion, collision.transform.position, collision.transform.rotation), waitTime);
            Destroy(Instantiate(scoreToSpawn, collision.transform.position + new Vector3(0, -1f, 0), q), waitTime);
        }
    }
    IEnumerator SpawnEnemyAgain(GameObject enemy)
    {
        enemy.SetActive(false);

        enemy.GetComponent<MeshRenderer>().material = jellyMaterials[Random.Range(0, 2)];

        yield return new WaitForSeconds(waitTime);
        enemy.SetActive(true);

        //Instantiate(enemyToSpawn, killPos, killRot);
        bulletCollission = false;
        Destroy(gameObject); // destroy bullet

    }
}

