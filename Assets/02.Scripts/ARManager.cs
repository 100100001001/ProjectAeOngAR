using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;


public class ARManager : MonoBehaviour
{
    public static ARManager instance; // 인스턴스화


    public ARRaycastManager arRaycater;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    // 바닥에 표시
    public GameObject[] indicatorTest;
    private Transform indicator;
    List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>();



    // 시간이 지남에 따라 생성되는 먼지
    public GameObject[] dust;
    private Transform dustTransform;

    private float dustXMin = -10f;
    private float dustXMax = 10f;

    private float dustYMin = -10f;
    private float dustYMax = 10f;

    private float dustScaleMin = 2;
    private float dustScaleMax = 5;

    private int dustLen = 2;

    
    public GameObject ttest;
    public TextMeshProUGUI t;


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
                indicatorTest[1].SetActive(false);
                indicator = indicatorTest[2].transform;
                PlaceIndicator();
                return;
            case Status.Evolution.YOUTH:
                indicatorTest[2].SetActive(false);
                indicator = indicatorTest[3].transform;
                PlaceIndicator();
                return;
            default:
                return;
        }

    }


    void PlaceIndicator()
    {
        arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), indicatorHits, TrackableType.Planes);

        if (indicatorHits.Count > 0)
        {
            indicator.position = indicatorHits[0].pose.position;
            indicator.rotation = indicatorHits[0].pose.rotation;



            dustTransform = dust[UnityEngine.Random.Range(0, dust.Length)].transform;


            float dustXPos = UnityEngine.Random.Range(dustXMin, dustXMax);
            float dustYPos = UnityEngine.Random.Range(dustYMin, dustYMax);
            float dustScale = UnityEngine.Random.Range(dustScaleMin, dustScaleMax);

            dustTransform.transform.localScale = new Vector3(dustScale, dustScale, dustScale);

            dustTransform.position = new Vector3(indicatorHits[0].pose.position.x, indicatorHits[0].pose.position.y, indicatorHits[0].pose.position.z);
            dustTransform.rotation = indicatorHits[0].pose.rotation;


            // test!!!!!!!!!!!!!
        }

    }
}

//public void PlaceIndicatorPrefab()
//{
//    Pose hitPose = IndicatorHits[0].pose;
//    Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
//}

