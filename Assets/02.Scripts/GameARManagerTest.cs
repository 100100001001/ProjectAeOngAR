using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class GameARManagerTest : MonoBehaviour
{
    public static GameARManagerTest instance; // 인스턴스화


    public ARRaycastManager arRaycater;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    // 바닥에 표시
    public GameObject indicatorTest;
    private Transform indicator;
    List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>();


    private float bombXMin = -5f;
    private float bombXMax = 5f;

    private float bombYMin = -5f;
    private float bombYMax = 5f;

    private float bombScaleMin = 0.5f;
    private float bombScaleMax = 0.9f;



    public GameObject ttest;
    private TextMeshProUGUI t;


    float time;

    void Start()
    {
        // indicatorTest[0].SetActive(true);
        indicator = indicatorTest.transform;
        PlaceIndicator();

        t = ttest.GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > 5)
        {

        }

        PlaceIndicator();
        t.text = indicator.name;

    }


    void PlaceIndicator()
    {
        arRaycater.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), indicatorHits, TrackableType.Planes);

        if (indicatorHits.Count > 0)
        {
            indicator.position = indicatorHits[0].pose.position;
            indicator.rotation = indicatorHits[0].pose.rotation;
        }

    }
}
