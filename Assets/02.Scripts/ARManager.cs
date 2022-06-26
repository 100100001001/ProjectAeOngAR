using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARManager : MonoBehaviour
{
    public static ARManager instance; // 인스턴스화


    public ARRaycastManager arRaycater;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    // 바닥에 표시
    public GameObject[] indicatorTest;
    private Transform indicator;
    List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>();

    void Start()
    {
        indicator = indicatorTest[0].transform;
    }

    void Update()
    {
        switch (Status.instance.evo)
        {
            case Status.Evolution.EGG:
                indicator = indicatorTest[0].transform;
                PlaceIndicator();
                return;
            case Status.Evolution.BABY:
                indicatorTest[0].SetActive(false);
                indicator = indicatorTest[1].transform;
                PlaceIndicator();
                return;
            case Status.Evolution.CHILD:
                return;
            case Status.Evolution.YOUTH:
                return;
            default:
                return;
        }

        //if (Status.instance.evo == Status.Evolution.EGG)
        //{
        //    indicatorTest[0].SetActive(false);
        //    indicator = indicatorTest[1].transform;
        //}

    }


    void PlaceIndicator()
    {
        arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), indicatorHits, TrackableType.Planes);

        if (indicatorHits.Count > 0)
        {
            indicator.position = indicatorHits[0].pose.position;
            indicator.rotation = indicatorHits[0].pose.rotation;
        }
    }

    //public void PlaceIndicatorPrefab()
    //{
    //    Pose hitPose = IndicatorHits[0].pose;
    //    Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
    //}


}