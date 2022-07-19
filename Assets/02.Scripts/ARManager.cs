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
    public static ARManager instance;   // ARManager 인스턴스화

    public ARRaycastManager arRaycater; // 평면을 인식하기 위한 ARRaycastManager 변수

    
    public GameObject[] indicator; // 평면에 표시될 오브젝트
    // Egg, Baby, Child, Youth, Ground(동산), EggBreak_Top, EggBreak_Bottom, EggBreak 순으로 들어가있다.

    private Transform indicatorTr; // 사용자의 손 터치로 평면에 표시된 오브젝트 위치가 변경되기 때문에 transform을 변수로 받아줌
    List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>(); // AR raycast에서 쏜 광선



    // 테스트 //
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
    bool textActive = true;                 // 텍스트 활성화 여부. 게임 시작할 때만 나와야하기 때문에 true


    void Start()
    {
        //indicator[4].SetActive(true);
        //indicatorTr = indicator[4].transform;
        //PlaceIndicator();


        // 캐릭터의 색상을 지정해주기 위해 각각의 MeshRenderer, Renderer컴포넌트를 가져온다
        eggMR = indicator[0].GetComponent<MeshRenderer>();
        babyMR = indicator[1].transform.GetChild(1).GetComponent<Renderer>();
        childMR = indicator[2].transform.GetChild(1).GetComponent<Renderer>();
        youthMR = indicator[3].transform.GetChild(1).GetComponent<Renderer>();
        eggbreakUpMR = indicator[5].GetComponent<MeshRenderer>();
        eggbreakBotMR = indicator[6].GetComponent<MeshRenderer>();

        indicator[0].SetActive(false);
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


    // 바닥 평면을 감지하는 메서드
    void PlaceIndicator()
    {
        // 화면 중앙에 레이를 쏴서 평면을 찾는다
        // TrackableType.Planes : 광선이 평면 유형과 교차하는 경우 광선이 맞은 것으로 간주
        arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), indicatorHits, TrackableType.Planes);

        // 감지가 됐다면 indicatorTr의 위치와 회전 정보를 indicatorHits의 pose로 가져온다
        if (indicatorHits.Count > 0)
        {
            indicatorTr.position = indicatorHits[0].pose.position;
            indicatorTr.rotation = indicatorHits[0].pose.rotation;
        }
    }


    // 캐릭터의 색상을 임의 지정하는 메서드
    void ChangeColor()
    {
        // 8가지 색상 중 임의의 색을 랜덤으로 가져오기 위해 0~8사이에서 랜덤한 숫자를 가져온다.
        int n = UnityEngine.Random.Range(0, 8);

        if (n == 0) color = new Color32(233, 200, 218, 255);      // 연두색 (원래 색상)
        else if (n == 1) color = new Color32(255, 255, 218, 255); // 노란색
        else if (n == 2) color = new Color32(255, 235, 218, 255); // 주황색
        else if (n == 3) color = new Color32(255, 218, 225, 255); // 빨간색
        else if (n == 4) color = new Color32(255, 218, 255, 255); // 분홍색
        else if (n == 5) color = new Color32(239, 218, 255, 255); // 보라색
        else if (n == 6) color = new Color32(218, 221, 255, 255); // 남색
        else if (n == 7) color = new Color32(218, 251, 255, 255); // 하늘색


        // 랜덤한 색상을 각각 적용시킨다.
        eggMR.material.SetColor("_Color", color);
        babyMR.material.SetColor("_Color", color);
        childMR.material.SetColor("_Color", color);
        youthMR.material.SetColor("_Color", color);
        eggbreakUpMR.material.SetColor("_Color", color);
        eggbreakBotMR.material.SetColor("_Color", color);
    }


    IEnumerator TextActive()
    {
        textActive = false;

        descriptiveText.text = "두 손을 꾹 누른 채 움직여 보세요\n또바기의 동산이 따라와요~!";
        yield return new WaitForSeconds(7f);

        descriptiveText.text = "또바기와 함께 즐거운 시간 보내세요 >.<";
        yield return new WaitForSeconds(5f);

        descriptiveText.gameObject.SetActive(false);
    }


}

//public void PlaceIndicatorPrefab()
//{
//    Pose hitPose = IndicatorHits[0].pose;
//    Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
//}

