using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerSlider : MonoBehaviour
{
    [SerializeField]
    Slider timerSlider; // 남은 시간을 시각화하기 위해 받은 슬라이더

    [SerializeField]
    TextMeshProUGUI timerText; // 남은 시간

    // 슬라이더 색을 변경하기 위해 이미지와 색상을 받아줌
    [SerializeField]
    Image fillImage;

    public Color32 normalFillColor;
    public Color32 warningFillColor;
    public float warningLimit;  // 시간이 얼마 안남은 걸 알려주기 위해 받는 변수

    public bool stopTimer;

    [SerializeField]
    TextMeshProUGUI gameOverText;


    string textTime;

    float curTime = 20f;
    float maxTime = 20f;


    public GameObject Buttons;


    void Start()
    {
        stopTimer = false;
        gameObject.GetComponent<Shoot>().enabled = true;

        gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TextMeshProUGUI>();
        gameOverText.gameObject.SetActive(false);

        timerSlider = GameObject.FindGameObjectWithTag("TimerSlider").GetComponent<Slider>();
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TextMeshProUGUI>();

        fillImage = GameObject.FindGameObjectWithTag("SliderFill").GetComponent<Image>();

        timerSlider.value = (float)curTime / (float)maxTime;
        fillImage.color = normalFillColor;

        Buttons.SetActive(false);


    }


    void Update()
    {
        curTime -= Time.deltaTime;
        textTime = "" + curTime.ToString("f0") + "초 남았어요";

        if (stopTimer == false)
        {
            timerText.text = textTime;
            timerSlider.value = (float)curTime / (float)maxTime;

        }

        if (timerSlider.value < ((warningLimit / 100) * timerSlider.maxValue))
        {
            fillImage.color = warningFillColor;
        }

        if (curTime <= 0 && stopTimer == false)  // On Game over
        {
            stopTimer = true;
            gameOverText.gameObject.SetActive(true);
            gameObject.GetComponent<Shoot>().enabled = false;
            //Destroy(timerSlider.gameObject);

            Buttons.SetActive(true);



            //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Jelly");
            //foreach (GameObject enemy in enemies)
            //    Destroy(enemy); // destroy all spiders in the scene

        }









    }
}

