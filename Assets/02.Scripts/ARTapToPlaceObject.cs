using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;

    private ARRaycastManager arOrigin; // 원점
    private Pose placementPose;        // 3D 포인트의 위치와 회전 확인
    private bool placementPoseValid = false;

    void Start()
    {
        arOrigin = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementPose() {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);
        // 첫번째 매개변수 : 화면 중앙에서 광선을 쏘려는 화면 포인트
        // 두번째 매개변수 : ARRaycastHit 객체 목록. 이 Hit객체는 광선이 물리적 표면에 닿는 물리적 공간의 모든 지점을 나타냄
        // 마지막 매개변수(선택) : 추적 가능한 유형

        placementPoseValid = hits.Count > 0; // hits 배열에 하나라도 있는 경우만 true
        if (placementPoseValid)
        {
            placementPose = hits[0].pose;
        }
    }

    // 업데이트 배치 표시
    private void UpdatePlacementIndicator()
    {
        // 배치 포즈가 유효한지 확인
        if (placementPoseValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }
}
