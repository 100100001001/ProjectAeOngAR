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

    public float curTime = 20f;
    float maxTime = 20f;


    public GameObject buttons;
    public GameObject targetImage;

    public TextMeshProUGUI t;



    // 게임이 끝나면 보물 상자가 나옴
    public GameObject treasureBox;
    public GameObject items;


    void Start()
    {
        stopTimer = false; // 타이머 유무
        gameObject.GetComponent<Shoot>().enabled = true; // Shoot 스크립트 실행

        gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TextMeshProUGUI>(); // GameOverText 태그를 가지고 있는 게임 오브젝트에서 TextMeshProUGUI 컴포넌트를 가져 옴
        gameOverText.gameObject.SetActive(false); // gameOverText는 게임오버 후 활성화 되어야하기 때문에 비활성화

        timerSlider = GameObject.FindGameObjectWithTag("TimerSlider").GetComponent<Slider>(); // TimerSlider 태그를 가지고 있는 게임 오브젝트에서 Slider 컴포넌트를 가져 옴
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TextMeshProUGUI>(); // TimerText 태그를 가지고 있는 게임 오브젝트에서 TextMeshProUGUI 컴포넌트를 가져 옴

        fillImage = GameObject.FindGameObjectWithTag("SliderFill").GetComponent<Image>(); // SliderFill 태그를 가지고 있는 게임 오브젝트에서 Image 컴포넌트를 가져 옴

        timerSlider.value = (float)curTime / (float)maxTime; // timerSlider의 값을 0~1 사이의 값으로 맞춰주기 위함
        fillImage.color = normalFillColor; // 슬라이더의 채

        buttons.SetActive(false);
    }


    void Update()
    {
        //t.text = "Update : " + curTime;

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

            targetImage.SetActive(false);
            buttons.SetActive(true);


            //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Jelly");
            //foreach (GameObject enemy in enemies)
            //    Destroy(enemy); // destroy all spiders in the scene

        }

    }

    public void Replay()
    {
        gameObject.GetComponent<Shoot>().enabled = true;

        curTime = 20f;
        timerSlider.value = (float)curTime / (float)maxTime;

        stopTimer = false;
        gameOverText.gameObject.SetActive(false);
        buttons.SetActive(false);

        Scoring.score = 0;
        fillImage.color = normalFillColor;

        targetImage.SetActive(true);

    }

    void Treasure()
    {
        treasureBox.SetActive(true);
        StartCoroutine(ItemsActive());


    }


    IEnumerator ItemsActive()
    {
        items.SetActive(true);
        yield return new WaitForSeconds(3f);
    }
}

