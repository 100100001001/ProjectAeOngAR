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



    //// 시간이 지남에 따라 생성되는 먼지
    //public GameObject[] dust;
    //private Transform dustTransform;

    //private float dustXMin = -0.001f;
    //private float dustXMax = 0.001f;

    //private float dustYMin = -0.002f;
    //private float dustYMax = 0.002f;

    //private float dustScaleMin = 0.005f;
    //private float dustScaleMax = 0.01f;

    //private int dustLen = 2;

    
    public GameObject ttest;
    public TextMeshProUGUI t;


    void Start()
    {
        indicator = indicatorTest[0].transform;
        //dustTransform = dust[0].transform;

        indicatorTest[1].SetActive(false);
        indicatorTest[2].SetActive(false);
        indicatorTest[3].SetActive(false);
    }

    void Update()
    {
        switch (Status.instance.evo)
        {
            case Status.Evolution.EGG:
                indicatorTest[0].SetActive(true);
                indicator = indicatorTest[0].transform;
                PlaceIndicator();
                return;
            case Status.Evolution.BABY:
                indicatorTest[0].SetActive(false);
                indicatorTest[1].SetActive(true);
                indicator = indicatorTest[1].transform;
                PlaceIndicator();
                return;
            case Status.Evolution.CHILD:
                indicatorTest[1].SetActive(false);
                indicatorTest[2].SetActive(true);
                indicator = indicatorTest[2].transform;
                PlaceIndicator();
                return;
            case Status.Evolution.YOUTH:
                indicatorTest[2].SetActive(false);
                indicatorTest[3].SetActive(true);
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


            //dustTransform.position = indicatorHits[0].pose.position;
            //dustTransform.rotation = indicatorHits[0].pose.rotation;


            //for (int i = 0; i < UnityEngine.Random.Range(0, dust.Length); i++)
            //{
            //    dustTransform = dust[UnityEngine.Random.Range(0, dust.Length)].transform;

            //    float dustXPos = UnityEngine.Random.Range(dustXMin, dustXMax);
            //    float dustYPos = UnityEngine.Random.Range(dustYMin, dustYMax);
            //    float dustScale = UnityEngine.Random.Range(dustScaleMin, dustScaleMax);

            //    dustTransform.transform.localScale = new Vector3(dustScale, dustScale, dustScale);

            //    dustTransform.position = new Vector3(indicatorHits[0].pose.position.x * dustXPos, indicatorHits[0].pose.position.y * dustYPos, indicatorHits[0].pose.position.z);
            //    dustTransform.rotation = indicatorHits[0].pose.rotation;

            //}


            // test!!!!!!!!!!!!!
        }

    }
}

//public void PlaceIndicatorPrefab()
//{
//    Pose hitPose = IndicatorHits[0].pose;
//    Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
//}

