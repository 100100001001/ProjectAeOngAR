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


    bool bulletCollission = false; // to avoid hittimg multiple dpiders with same bullet
    int jellyScoreNum = 1;



    Color color;

    public Color32 gJellyScoreColor;
    public Color32 oJellyScoreColor;
    public Color32 pJellyScoreColor;


    public Material[] jellyMaterials;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Jelly" && bulletCollission == false)
        {
            //Destroy(collision.transform.gameObject); // destroy spider

            if (collision.gameObject.GetComponent<MeshRenderer>().material.name == "GreenJelly (Instance)")
            {
                color = gJellyScoreColor;
                jellyScoreNum = 5;
            }
            else if (collision.gameObject.GetComponent<MeshRenderer>().material.name == "OrangeJelly (Instance)")
            {
                color = oJellyScoreColor;
                jellyScoreNum = 10;
            }
            else if (collision.gameObject.GetComponent<MeshRenderer>().material.name == "PurpleJelly (Instance)")
            {
                color = pJellyScoreColor;
                jellyScoreNum = 15;

            }


            Scoring.score += jellyScoreNum;

            bulletCollission = true;

            //killPos = collision.transform.position;
            //killRot = collision.transform.rotation;
            StartCoroutine(SpawnEnemyAgain(collision.gameObject));



            Quaternion q = new Quaternion(0, -1, 0, 0);
            q *= collision.gameObject.transform.rotation;

            Destroy(Instantiate(explosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation), 3f);

            //scoreToSpawn.transform.position = collision.gameObject.transform.position;
            //t.text = ""+scoreToSpawn.transform.position + "/" + collision.gameObject.transform.position;
            StartCoroutine(ScoreToSpawnTime());

            //Destroy(Instantiate(scoreToSpawn, collision.gameObject.transform.position + new Vector3(0, -1f, 0), q), 3f);
        }
    }
    IEnumerator SpawnEnemyAgain(GameObject enemy)
    {
        enemy.SetActive(false);

        enemy.GetComponent<MeshRenderer>().material = jellyMaterials[Random.Range(0, 3)];

        yield return new WaitForSeconds(Random.Range(0, 3));


        enemy.SetActive(true);

        //Instantiate(enemyToSpawn, killPos, killRot);
        bulletCollission = false;
        Destroy(gameObject); // destroy bullet

    }

    IEnumerator ScoreToSpawnTime()
    {
        scoreToSpawn.GetComponentInChildren<TextMeshProUGUI>().text = "+" + jellyScoreNum;
        scoreToSpawn.GetComponentInChildren<TextMeshProUGUI>().color = color;

        scoreToSpawn.SetActive(true);

        yield return new WaitForSeconds(2f);
        scoreToSpawn.SetActive(false);
    }
}

