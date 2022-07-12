using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.IO;

public class ARPlacement : MonoBehaviour
{

    public GameObject arObjectToSpawn;      // 몬스터 생성
    public GameObject placementIndicator;   // 바닥 표시기
    public GameObject shoot;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;



    //public string m_Path = @"C:\Users\user\Desktop";
    //public string m_FilePrefix = "screenshot";
    //private string m_FilePath;



    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        shoot.SetActive(false);
    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {
        if (spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
            shoot.SetActive(true);
        }

        //if (spawnedObject != null) spawnedObject.transform.position += new Vector3(0, 0, -0.001f);


        UpdatePlacementPose();
        UpdatePlacementIndicator();


    }
    void UpdatePlacementIndicator()
    {
        if(spawnedObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if(placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    void ARPlaceObject()
    {
        spawnedObject = Instantiate(arObjectToSpawn, PlacementPose.position, PlacementPose.rotation);
    }



    //public void Save()
    //{
    //    ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
    //    "Screenshot" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png"));
    //}

}

