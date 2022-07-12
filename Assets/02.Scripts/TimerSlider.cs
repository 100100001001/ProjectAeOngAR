using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerSlider : MonoBehaviour
{
    [SerializeField]
    Slider timerSlider;

    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField]
    Image fillImage;
    public Color32 normalFillColor;
    public Color32 warningFillColor;
    public float warningLimit;  // as a percentage

    public bool stopTimer;

    [SerializeField]
    TextMeshProUGUI gameOverText;


    string textTime;

    float curTime = 2f;
    float maxTime = 2f;


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


            //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Jelly");
            //foreach (GameObject enemy in enemies)
            //    Destroy(enemy); // destroy all spiders in the scene

        }









    }
}

