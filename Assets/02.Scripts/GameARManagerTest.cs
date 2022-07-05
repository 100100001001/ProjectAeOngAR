using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class GameARManagerTest : MonoBehaviour
{
    public static GameARManagerTest instance; // 인스턴스화


    public ARRaycastManager arRaycater;
    List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>();


    public GameObject jelly;
    private Transform indicator;
    private GameObject[] spawnedJelly;

    private Pose PlacementPose;






    private float jellyXMin = -0.5f;
    private float jellyXMax = 1.5f;

    private float jellyYMin = 1.5f;
    private float jellyYMax = 2f;

    private float jellyScaleMin = 0.05f;
    private float jellyScaleMax = 0.3f;

    private Vector3 poolPosition = new Vector3(-25, 0, 0);
    private Vector3 ro = new Vector3(0, 180, 0);




    public GameObject ttest;
    private TextMeshProUGUI t;




    float time;




    void Start()
    {
        spawnedJelly = new GameObject[10];
        // jelly[0].SetActive(true);
        //indicator = jelly.transform;
        PlaceIndicator();
        //PlaceIndicatorPrefab();

        //t = ttest.GetComponent<TextMeshProUGUI>();



        for (int i = 0; i < 10; i++)
        {
            spawnedJelly[i] = Instantiate(jelly, poolPosition, Quaternion.Euler(ro));
        }

    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > 10)
        {
            //spawnedJelly = Instantiate(jelly, PlacementPose.position, PlacementPose.rotation);

            time = 0;
        }




        PlaceIndicator();
        StartCoroutine(SetJelly());



        //PlaceIndicatorPrefab();
        //t.text = indicator.name;

    }


    void PlaceIndicator()
    {
        arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), indicatorHits, TrackableType.Planes);

        if (indicatorHits.Count > 0)
        {
            PlacementPose.position = indicatorHits[0].pose.position;
            PlacementPose.rotation = indicatorHits[0].pose.rotation;
        }

    }

    IEnumerator SetJelly()
    {
        for (int i = 0; i < 10; i++)
        {
            
            float jellyXPos = Random.Range(jellyXMin, jellyXMax);
            float jellyYPos = Random.Range(jellyYMin, jellyYMax);
            float jellyScale = Random.Range(jellyScaleMin, jellyScaleMax);

            jelly.transform.localScale = new Vector3(jellyScale, jellyScale, jellyScale);
            Vector3 jellyPos = new Vector3(PlacementPose.position.x * jellyXPos, PlacementPose.position.y * jellyYPos, PlacementPose.position.z);
            

            
            //spawnedJelly[i].transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
            spawnedJelly[i].transform.SetPositionAndRotation(jellyPos, Quaternion.Euler(ro));
        }


        yield return new WaitForSeconds(0);


        // float jellyXPos = Random.Range(jellyXMin, jellyXMax);
        // float jellyYPos = Random.Range(jellyYMin, jellyYMax);
        // float jellyScale = Random.Range(jellyScaleMin, jellyScaleMax);

        // jelly.transform.localScale = new Vector3(jellyScale, jellyScale, jellyScale);
        // Vector3 jellyPos = new Vector3(PlacementPose.position.x * jellyXPos, PlacementPose.position.y * jellyYPos, PlacementPose.position.z);



        // for (int i = 0; i < 5; i++)
        // {
        //     spawnedJelly[i].transform.SetPositionAndRotation(jellyPos, PlacementPose.rotation);
        // }


        //jelly.transform.SetPositionAndRotation(jellyPos, PlacementPose.rotation);


        //spawnedJelly = Instantiate(jelly, PlacementPose.position, PlacementPose.rotation);
        //spawnedJelly = Instantiate(jelly, jellyPos, Quaternion.identity);

    }







    // public void PlaceIndicatorPrefab()
    // {

    //     arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), indicatorHits, TrackableType.Planes);

    //     if (indicatorHits.Count > 0)
    //     {
    //         Pose hitPose = indicatorHits[0].pose;
    //         Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);

    //     }


    // }



}


