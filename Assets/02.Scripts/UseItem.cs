using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    public Slider slider;

    float curTime = 10f; // 슬라이더의 Value값을 조정해주기 위한 시간 변수
    float maxTime = 10f; // 슬라이더 Value의 최대값

    bool stopTimer = true;

    public GameObject food3d;
    public GameObject foodButton;

    void Update()
    {
        if (stopTimer == false)
        {
            curTime -= Time.deltaTime;
            slider.value = (float)curTime / (float)maxTime; // 슬라이더의 Value를 계산
            gameObject.GetComponent<Button>().interactable = false;
        }

        if (curTime <= 0 && stopTimer == false)  
        {
            stopTimer = true;
            curTime = 10f;
            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void Use()
    {
        StatusBar.instance.HappyValue(true, 20);
        StatusBar.instance.HungerValue(true, 20);

        stopTimer = false;

        StartCoroutine(SetActiveFood(gameObject.name));

    }

    IEnumerator SetActiveFood(string food)
    {

        for (int i = 0; i < foodButton.transform.childCount; i++)
        {
            foodButton.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
        }


        if (food == "Apple") food3d.transform.GetChild(0).gameObject.SetActive(true);
        else if (food == "Cake") food3d.transform.GetChild(1).gameObject.SetActive(true);
        else if (food == "Milk") food3d.transform.GetChild(2).gameObject.SetActive(true);
        else if (food == "Juice") food3d.transform.GetChild(3).gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        for (int i=0; i < food3d.transform.childCount; i++)
        {
            food3d.transform.GetChild(i).gameObject.SetActive(false);
        }


        for (int i = 0; i < foodButton.transform.childCount; i++)
        {
            foodButton.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
        }


    }


}
