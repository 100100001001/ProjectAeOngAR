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


    // 바닥에 표시
    public GameObject[] indicator;
    private Transform indicatorTr;
    List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>();


    public GameObject subText;


    float time;


    public GameObject shoot;

    MeshRenderer[] jellyMeshRenderers;
    public Material[] jellyMaterials;

    int jellyMtNumber;


    void Start()
    {
        shoot.SetActive(false);
        subText.SetActive(true);


        //indicator[0].SetActive(true);
        indicator[1].SetActive(false);

        //indicatorTr = indicator[0].transform;
        PlaceIndicator();


        jellyMeshRenderers = indicator[1].transform.GetComponentsInChildren<MeshRenderer>();

    }

    void Update()
    {
        if (Input.touchCount > 1)
        {
            subText.SetActive(false);
            //indicator[0].SetActive(false);
            shoot.SetActive(true);


            foreach (MeshRenderer mr in jellyMeshRenderers)
            {
                jellyMtNumber = Random.Range(0, 3);
                mr.material = jellyMaterials[jellyMtNumber];
            }

            indicator[1].SetActive(true);

            indicatorTr = indicator[1].transform;
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
            indicatorTr.position = indicatorHits[0].pose.position;
            indicatorTr.rotation = indicatorHits[0].pose.rotation;
        }

    }

}
