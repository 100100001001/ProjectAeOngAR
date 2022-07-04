using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class GameARManager : MonoBehaviour
{
    public static GameARManager instance; // 인스턴스화


    public ARRaycastManager arRaycater;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    // 바닥에 표시
    public GameObject[] indicatorTest;
    private Transform indicator;
    List<ARRaycastHit> indicatorHits = new List<ARRaycastHit>();



    public GameObject ttest;
    private TextMeshProUGUI t;


    float time;

    void Start()
    {
        // indicatorTest[0].SetActive(true);
        indicator = indicatorTest[0].transform;
        PlaceIndicator();

        t = ttest.GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > 5)
        {
            int a = Random.Range(0, 3);


            if (a == 0) indicator = indicatorTest[0].transform;
            else if (a == 1) indicator = indicatorTest[1].transform;
            else if (a == 2) indicator = indicatorTest[2].transform;

            time = 0;


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
