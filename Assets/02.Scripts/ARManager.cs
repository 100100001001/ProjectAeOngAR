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



    private MeshRenderer prefabsMR;

    Color color;




    void Start()
    {
        indicatorTest[4].SetActive(true);
        indicator = indicatorTest[4].transform;
        PlaceIndicator();


        prefabsMR = indicatorTest[0].GetComponent<MeshRenderer>();



        ChangeColorrr();

        //dustTransform = dust[0].transform;

        //indicatorTest[1].SetActive(false);
        //indicatorTest[2].SetActive(false);
        //indicatorTest[3].SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount > 1)// && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            indicatorTest[4].SetActive(true);
            indicator = indicatorTest[4].transform;
            PlaceIndicator();




        }

        switch (Status.instance.evo1)
        {
            case Status.Evolution1.EGG:
                indicatorTest[0].SetActive(true);
                return;
            case Status.Evolution1.BABY:
                indicatorTest[0].SetActive(false);
                indicatorTest[1].SetActive(true);
                return;
            case Status.Evolution1.CHILD:
                indicatorTest[1].SetActive(false);
                indicatorTest[2].SetActive(true);
                return;
            case Status.Evolution1.YOUTH:
                indicatorTest[2].SetActive(false);
                indicatorTest[3].SetActive(true);
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


    void ChangeColorrr()
    {

        int n = UnityEngine.Random.Range(0, 8);

        if (n == 0) color = new Color32(255, 200, 240, 255);
        else if (n == 1) color = new Color32(255, 205, 200, 255);
        else if (n == 2) color = new Color32(255, 250, 200, 255);
        else if (n == 3) color = new Color32(200, 255, 205, 255);
        else if (n == 4) color = new Color32(200, 255, 234, 255);
        else if (n == 5) color = new Color32(200, 251, 255, 255);
        else if (n == 6) color = new Color32(200, 213, 255, 255);
        else if (n == 7) color = new Color32(220, 200, 255, 255);


        prefabsMR.material.SetColor("_Color", color);
    }
}

//public void PlaceIndicatorPrefab()
//{
//    Pose hitPose = IndicatorHits[0].pose;
//    Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
//}

