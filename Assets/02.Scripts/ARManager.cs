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

    // 바닥에 표시
    public GameObject[] indicator;
    private Transform indicatorTr;
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


    // 랜덤 색상을 적용하기 위해 캐릭터 각각의 MeshRenderer, Renderer 변수 만듦
    private MeshRenderer eggMR;
    private Renderer babyMR;
    private Renderer childMR;
    private Renderer youthMR;
    private MeshRenderer eggbreakUpMR;
    private MeshRenderer eggbreakBotMR;
    Color color;


    public TextMeshProUGUI descriptiveText; // 게임 시작할 때 나올 설명 텍스트
    bool textActive = true; // 텍스트 활성화여부;

    void Start()
    {
        indicator[4].SetActive(true);
        indicatorTr = indicator[4].transform;
        PlaceIndicator();


        eggMR = indicator[0].GetComponent<MeshRenderer>();
        babyMR = indicator[1].transform.GetChild(1).GetComponent<Renderer>();
        childMR = indicator[2].transform.GetChild(1).GetComponent<Renderer>();
        youthMR = indicator[3].transform.GetChild(1).GetComponent<Renderer>();
        eggbreakUpMR = indicator[5].GetComponent<MeshRenderer>();
        eggbreakBotMR = indicator[6].GetComponent<MeshRenderer>();

        indicator[0].SetActive(true);


        indicator[1].SetActive(false);
        indicator[2].SetActive(false);
        indicator[3].SetActive(false);
        indicator[7].SetActive(false);


        descriptiveText.text = "두 손가락으로 동시에 터치해서 또바기를 불러오세요!";
        descriptiveText.gameObject.SetActive(true);


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
            indicator[4].SetActive(true);
            indicatorTr = indicator[4].transform;
            PlaceIndicator();

            if (textActive) StartCoroutine(TextActive());
            textActive = false;
        }

        switch (Status.instance.evo1)
        {
            case Status.Evolution1.EGG:

                indicator[0].SetActive(true);
                return;
            case Status.Evolution1.BABY:


                indicator[7].SetActive(false);

                indicator[1].SetActive(true);

                return;
            case Status.Evolution1.CHILD:


                indicator[1].SetActive(false);
                indicator[2].SetActive(true);
                return;
            case Status.Evolution1.YOUTH:
                indicator[2].SetActive(false);
                indicator[3].SetActive(true);
                return;

            case Status.Evolution1.BREAKEGG:
                indicator[0].SetActive(false);
                indicator[7].SetActive(true);
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
            indicatorTr.position = indicatorHits[0].pose.position;
            indicatorTr.rotation = indicatorHits[0].pose.rotation;


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


    IEnumerator TextActive()
    {
        descriptiveText.text = "두 손을 꾹 누른 채 움직여 보세요\n또바기의 동산이 따라와요~!";

        yield return new WaitForSeconds(10f);

        descriptiveText.text = "또바기와 함께 즐거운 시간 보내세요 >.<";

        yield return new WaitForSeconds(5f);

        textActive = false;

        descriptiveText.gameObject.SetActive(false);
    }


}

//public void PlaceIndicatorPrefab()
//{
//    Pose hitPose = IndicatorHits[0].pose;
//    Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
//}

