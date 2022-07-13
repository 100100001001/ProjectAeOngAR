using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerSlider : MonoBehaviour
{
    [SerializeField]
    Slider timerSlider; // ���� �ð��� �ð�ȭ�ϱ� ���� ���� �����̴�

    [SerializeField]
    TextMeshProUGUI timerText; // ���� �ð�

    // �����̴� ���� �����ϱ� ���� �̹����� ������ �޾���
    [SerializeField]
    Image fillImage;

    public Color32 normalFillColor;
    public Color32 warningFillColor;
    public float warningLimit;  // �ð��� �� �ȳ��� �� �˷��ֱ� ���� �޴� ����

    public bool stopTimer;

    [SerializeField]
    TextMeshProUGUI gameOverText;


    string textTime;

    public float curTime = 20f;
    float maxTime = 20f;


    public GameObject buttons;
    public GameObject targetImage;

    public TextMeshProUGUI t;



    // ������ ������ ���� ���ڰ� ����
    public GameObject treasureBox;
    public GameObject items;


    void Start()
    {
        stopTimer = false; // Ÿ�̸� ����
        gameObject.GetComponent<Shoot>().enabled = true; // Shoot ��ũ��Ʈ ����

        gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TextMeshProUGUI>(); // GameOverText �±׸� ������ �ִ� ���� ������Ʈ���� TextMeshProUGUI ������Ʈ�� ���� ��
        gameOverText.gameObject.SetActive(false); // gameOverText�� ���ӿ��� �� Ȱ��ȭ �Ǿ���ϱ� ������ ��Ȱ��ȭ

        timerSlider = GameObject.FindGameObjectWithTag("TimerSlider").GetComponent<Slider>(); // TimerSlider �±׸� ������ �ִ� ���� ������Ʈ���� Slider ������Ʈ�� ���� ��
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TextMeshProUGUI>(); // TimerText �±׸� ������ �ִ� ���� ������Ʈ���� TextMeshProUGUI ������Ʈ�� ���� ��

        fillImage = GameObject.FindGameObjectWithTag("SliderFill").GetComponent<Image>(); // SliderFill �±׸� ������ �ִ� ���� ������Ʈ���� Image ������Ʈ�� ���� ��

        timerSlider.value = (float)curTime / (float)maxTime; // timerSlider�� ���� 0~1 ������ ������ �����ֱ� ����
        fillImage.color = normalFillColor; // �����̴��� ä

        buttons.SetActive(false);
    }


    void Update()
    {
        //t.text = "Update : " + curTime;

        curTime -= Time.deltaTime;
        textTime = "" + curTime.ToString("f0") + "�� ���Ҿ��";

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

