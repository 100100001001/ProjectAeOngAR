using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerSlider : MonoBehaviour
{

    public Slider timerSlider;
    public TextMeshProUGUI timerText;

    public float gameTime = 20.0f;

    public Image fillImage;
    public Color32 normalFillColor;
    public Color32 warningFillColor;
    public float warningLimit;  // as a percentage

    public bool stopTimer;

    public TextMeshProUGUI gameOverText;

    void Start()
    {
        stopTimer = false;
        gameObject.GetComponent<Shoot>().enabled = true;

        gameOverText.gameObject.SetActive(false);


        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
        fillImage.color = normalFillColor;

    }


    void Update()
    {
        gameTime -= Time.deltaTime;

        string textTime = "남은 시간 : " + gameTime.ToString("f0") + "초";

        if (stopTimer == false)
        {
            timerText.text = textTime;
            timerSlider.value = gameTime;
        }

        if (timerSlider.value < ((warningLimit / 100) * timerSlider.maxValue))
        {
            fillImage.color = warningFillColor;
        }

        if (gameTime <= 0 && stopTimer == false)  // On Game over
        {
            stopTimer = true;
            gameObject.GetComponent<Shoot>().enabled = false;
            Destroy(timerSlider.gameObject);
            gameOverText.gameObject.SetActive(true);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Jelly");
            foreach (GameObject enemy in enemies)
                Destroy(enemy); // destroy all spiders in the scene

        }





    }
}
