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



    private MeshRenderer eggMR;
    private Renderer babyMR;
    private Renderer childMR;
    private Renderer youthMR;
    private MeshRenderer eggbreakUpMR;
    private MeshRenderer eggbreakBotMR;

    Color color;



    public GameObject eggBreakParticleObject;


    void Start()
    {
        indicatorTest[4].SetActive(true);
        indicator = indicatorTest[4].transform;
        PlaceIndicator();


        eggMR = indicatorTest[0].GetComponent<MeshRenderer>();
        babyMR = indicatorTest[1].transform.GetChild(1).GetComponent<Renderer>();
        childMR = indicatorTest[2].transform.GetChild(1).GetComponent<Renderer>();
        youthMR = indicatorTest[3].transform.GetChild(1).GetComponent<Renderer>();
        eggbreakUpMR = indicatorTest[5].GetComponent<MeshRenderer>();
        eggbreakBotMR = indicatorTest[6].GetComponent<MeshRenderer>();

        indicatorTest[0].SetActive(true);


        indicatorTest[1].SetActive(false);
        indicatorTest[2].SetActive(false);
        indicatorTest[3].SetActive(false);
        indicatorTest[7].SetActive(false);


        ChangeColor();

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


                indicatorTest[7].SetActive(false);

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

            case Status.Evolution1.BREAKEGG:
                indicatorTest[0].SetActive(false);
                indicatorTest[7].SetActive(true);
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


    void ChangeColor()
    {

        int n = UnityEngine.Random.Range(0, 8);

        if (n == 0) color = new Color32(233, 200, 218, 255);      // 연두색 (원래 색상)
        else if (n == 1) color = new Color32(255, 255, 218, 255); // 노란색
        else if (n == 2) color = new Color32(255, 235, 218, 255); // 주황색
        else if (n == 3) color = new Color32(255, 218, 225, 255); // 빨간색
        else if (n == 4) color = new Color32(255, 218, 255, 255); // 분홍색
        else if (n == 5) color = new Color32(239, 218, 255, 255); // 보라색
        else if (n == 6) color = new Color32(218, 221, 255, 255); // 남색
        else if (n == 7) color = new Color32(218, 251, 255, 255); // 하늘색

        eggMR.material.SetColor("_Color", color);
        babyMR.material.SetColor("_Color", color);
        childMR.material.SetColor("_Color", color);
        youthMR.material.SetColor("_Color", color);
        eggbreakUpMR.material.SetColor("_Color", color);
        eggbreakBotMR.material.SetColor("_Color", color);
    }


    IEnumerator EggBreakParticlePlay()
    {
        eggBreakParticleObject.SetActive(true);

        yield return new WaitForSeconds(5f);


    }

}

//public void PlaceIndicatorPrefab()
//{
//    Pose hitPose = IndicatorHits[0].pose;
//    Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
//}

