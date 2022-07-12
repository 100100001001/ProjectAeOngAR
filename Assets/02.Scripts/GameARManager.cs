using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class GameARManager : MonoBehaviour
{
    //public static GameARManager instance; // 인스턴스화


    public ARRaycastManager arRaycater;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    // 바닥에 표시
    public GameObject[] indicatorTest;
    private Transform indicator;
    List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>();


    public TextMeshProUGUI t;


    float time;


    public GameObject shoot;

    MeshRenderer[] jellyMeshRenderers;
    public Material[] jellyMaterials;

    int jellyMtNumber;



    void Start()
    {
        shoot.SetActive(false);


        indicatorTest[0].SetActive(true);
        indicatorTest[1].SetActive(false);

        indicator = indicatorTest[0].transform;
        PlaceIndicator();


        jellyMeshRenderers = indicatorTest[1].transform.GetComponentsInChildren<MeshRenderer>();

    }

    void Update()
    {
        if (Input.touchCount > 1)
        {
            indicatorTest[0].SetActive(false);
            shoot.SetActive(true);


            foreach (MeshRenderer mr in jellyMeshRenderers)
            {
                jellyMtNumber = Random.Range(0, 3);
                mr.material = jellyMaterials[jellyMtNumber];
            }

            indicatorTest[1].SetActive(true);

            indicator = indicatorTest[1].transform;
            PlaceIndicator();

        }

    }


    void PlaceIndicator()
    {
        //var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        //List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>();

        //arRaycater.Raycast(screenCenter, indicatorHits, TrackableType.Planes);

        //if (indicatorHits.Count > 0)
        //{
        //    indicator.position = hits[0].pose.position;
        //    indicator.rotation = hits[0].pose.rotation;
        //}


        arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), indicatorHits, TrackableType.Planes);

        if (indicatorHits.Count > 0)
        {
            indicator.position = indicatorHits[0].pose.position;
            indicator.rotation = indicatorHits[0].pose.rotation;
        }

    }

}
