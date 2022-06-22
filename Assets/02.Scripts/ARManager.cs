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

    void Start()
    {

    }

    void Update()
    {
        PlaceIndicator();
    }

    #region 바닥 표시기

    public Transform Indicator;
    //public GameObject spawnPrefab;
    List<ARRaycastHit> IndicatorHits = new List<ARRaycastHit>();

    void PlaceIndicator()
    {
        arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), IndicatorHits, TrackableType.Planes);

        if (IndicatorHits.Count > 0)
        {
            Indicator.position = IndicatorHits[0].pose.position;
            Indicator.rotation = IndicatorHits[0].pose.rotation;
        }
    }

    //public void PlaceIndicatorPrefab()
    //{
    //    Pose hitPose = IndicatorHits[0].pose;
    //    Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
    //}

    #endregion

}