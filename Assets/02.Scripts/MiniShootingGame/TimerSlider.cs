using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TimerSlider : MonoBehaviour
{
    [Header("--- �����̴� ---")]

    [SerializeField]
    Slider timerSlider; // ���� �ð��� �ð�ȭ�ϱ� ���� ���� �����̴�

    [SerializeField]
    TextMeshProUGUI timerText; // ���� �ð�

    [SerializeField]
    Image fillImage; // �����̴� ���� �����ϱ� ���� �̹����� ������ �޾���

    public Color32 normalFillColor;  // �����̴��� �⺻ ����
    public Color32 warningFillColor; // �ð��� �� �ȳ����� �� ������ ��� ����
    public float warningLimit;       // ��� �� �ð��� ������ ����

    public bool stopTimer;           // Ÿ�̸� ����

    [SerializeField]
    TextMeshProUGUI gameOverText; // ���ӿ����� ���� �ؽ�Ʈ

    string textTime;              // �ð��� �˷��ֱ� ���� �ؽ�Ʈ

    public float curTime = 20f;   // �����̴��� Value���� �������ֱ� ���� �ð� ����
    float maxTime = 20f;          // �����̴� Value�� �ִ밪

    [Header("--- ���� ���� ---")]
    public TextMeshProUGUI gameStartText;

    [Header("--- ���� �� ---")]
    public GameObject[] jellies;
    public GameObject targetImage; // �����ϴ� �� ȭ�� �߾ӿ� ����� ���� �̹���

    [Header("--- ���� ���� �� ---")]

    public GameObject buttons;     // �ٽ� ���۰� �����ϱ� ��ư�� �ִ� ���� ������Ʈ

    public TextMeshProUGUI t;      // Testing!!!!!!!!!

    public GameObject treasureBox; // ������ ������ ���� ���� ����
    public GameObject items;       // ���� ���ڸ� ���� ������ ������

    public Texture[] milkTextures; // ���� �������� ������ �������ֱ� ���� �ؽ�ó���� �޾���

    private Animator ani;          // ���� ������ �ִϸ��̼�
    int treasureTouchCnt = 0;      // ���� ���ڰ� ��ġ�ϸ� ���� �� �ֵ��� ��ġ ī��Ʈ�� ������
    public TextMeshProUGUI treasureMessage; // ���� ���ڿ� ���õ� �˸� �޽���


    public static bool getMilk = false; // ������ ȹ�� ����
    public static bool getFood = false; // ������ ȹ�� ����
    public static int milkNumber;      // ������ �̸� ��ȯ
    public static string foodName;      // ������ �̸� ��ȯ


    void Start()
    {

        gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TextMeshProUGUI>(); // GameOverText �±׸� ������ �ִ� ���� ������Ʈ���� TextMeshProUGUI ������Ʈ�� ���� ��
        gameOverText.gameObject.SetActive(false); // gameOverText�� ���ӿ��� �� Ȱ��ȭ �Ǿ���ϱ� ������ ��Ȱ��ȭ

        timerSlider = GameObject.FindGameObjectWithTag("TimerSlider").GetComponent<Slider>(); // TimerSlider �±׸� ������ �ִ� ���� ������Ʈ���� Slider ������Ʈ�� ���� ��
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TextMeshProUGUI>(); // TimerText �±׸� ������ �ִ� ���� ������Ʈ���� TextMeshProUGUI ������Ʈ�� ���� ��

        fillImage = GameObject.FindGameObjectWithTag("SliderFill").GetComponent<Image>(); // SliderFill �±׸� ������ �ִ� ���� ������Ʈ���� Image ������Ʈ�� ���� ��

        ani = treasureBox.GetComponent<Animator>();

        fillImage.color = normalFillColor; // �����̴��� ä�� ���� �⺻ �������� ��������
        treasureBox.SetActive(false);
        treasureMessage.gameObject.SetActive(false);
        buttons.SetActive(false);
        items.SetActive(false);



        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Jelly");
        //foreach (GameObject enemy in enemies)
        //    enemy.SetActive(true);

        foreach (GameObject jelly in jellies) jelly.SetActive(true);



        StartCoroutine(GameStartMessage());

    }


    void OnEnable()
    {

        curTime = 20f;   // �����̴��� Value���� �������ֱ� ���� �ð� ����



        timerSlider.value = (float)curTime / (float)maxTime; // timerSlider�� ���� 0~1 ������ ������ �����ֱ� ����
        fillImage.color = normalFillColor; // �����̴��� ä�� ���� �⺻ �������� ��������


        timerSlider.gameObject.SetActive(true);
        buttons.SetActive(false);
        treasureBox.SetActive(false);
        treasureMessage.gameObject.SetActive(false);
        items.SetActive(false);
        gameOverText.gameObject.SetActive(false);


        Scoring.score = 0;
        fillImage.color = normalFillColor;

        targetImage.SetActive(true);
        treasureBox.SetActive(false);



        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Jelly");
        //foreach (GameObject enemy in enemies)
        //    enemy.SetActive(true);

        foreach (GameObject jelly in jellies) jelly.SetActive(true);


        StartCoroutine(GameStartMessage());
    }


    void Update()
    {
        //t.text = "Update : " + curTime;

        textTime = "" + curTime.ToString("f0") + "�� ���Ҿ��";

        if (stopTimer == false) // Ÿ�̸� �۵� ������ ��
        {
            curTime -= Time.deltaTime; // �ð��� ���


            timerText.text = textTime;
            timerSlider.value = (float)curTime / (float)maxTime; // �����̴��� Value�� ���
        }

        if (timerSlider.value < ((warningLimit / 100) * timerSlider.maxValue)) // ��� �ð� ���
        {
            fillImage.color = warningFillColor;
        }

        if (curTime <= 0 && stopTimer == false)  // ���ӿ��� ����
        {
            stopTimer = true;
            gameOverText.gameObject.SetActive(true);
            gameObject.GetComponent<Shoot>().enabled = false; // �Ѿ��� ������ �ʰ� �ϱ� ���� Shoot ��ũ��Ʈ ��Ȱ��ȭ
            timerSlider.gameObject.SetActive(false);
            //Destroy(timerSlider.gameObject);

            targetImage.SetActive(false);


            // "Jelly" �±׸� ���� ���ӿ�����Ʈ���� ��Ȱ��ȭ����
            //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Jelly");
            //foreach (GameObject enemy in enemies)
            //    enemy.SetActive(false);

            //foreach (GameObject jelly in jellies) jelly.SetActive(false);

            Treasure(); // ���� ���� 

        }
    }


    void Treasure()
    {
        treasureBox.SetActive(true); // �������� Ȱ��ȭ

        treasureMessage.text = "�α��α��α�";
        treasureMessage.gameObject.SetActive(true); // ���� ���� �� �޽��� Ȱ��ȭ

        StartCoroutine(ItemsActive());



        //if (Input.touchCount > 0)
        //{
        //    // ���� ��ġ ��ǥ���� ���� ����
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    RaycastHit hit;

        //    // ��ġ���� �� ��Ÿ���� ȿ��
        //    if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Player")
        //    {
        //        if (Input.touchCount > 1)
        //        {
        //            ani.SetTrigger("treasure");
        //            StartCoroutine(ItemsActive());


        //            treasureTouchCnt++;
        //            t.text = "" + treasureTouchCnt;
        //            //if (treasureTouchCnt > 5)
        //            //{


        //            //}

        //        }
        //    }
        //}


    }

    IEnumerator ItemsActive()
    {
        yield return new WaitForSeconds(1f);
        ani.SetTrigger("treasure");
        yield return new WaitForSeconds(0.5f);
        treasureBox.transform.GetChild(4).gameObject.SetActive(true);

        int n = Random.Range(0, 1);

        if (n == 0)
        {
            getMilk = true;

            yield return new WaitForSeconds(1.5f);
            items.SetActive(true);
            milkNumber = Random.Range(0, milkTextures.Length);
            items.GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", milkTextures[milkNumber]);

            treasureMessage.text = "��~ �������� ������!";

            yield return new WaitForSeconds(1f);
        }

        else if (n == 1)
        {
            
            getFood = true;


            // �����ؾ���~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            yield return new WaitForSeconds(1.5f);
            items.SetActive(true);
            int milkN = Random.Range(0, milkTextures.Length);
            items.GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", milkTextures[milkN]);
            foodName = milkTextures[n].name;

            treasureMessage.text = "��~ �������� ������!";

            yield return new WaitForSeconds(3f);
        }

        else
        {
            treasureMessage.text = "�ǹٱⰡ ��ſ����~ >.<~";
            yield return new WaitForSeconds(3f);
        }

        treasureBox.transform.GetChild(4).gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        treasureMessage.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        buttons.SetActive(true);

        StatusBar.instance.ActiveValue(true, 20f);
    }


    IEnumerator GameStartMessage()
    {
        gameStartText.gameObject.SetActive(true);
        gameStartText.text = "ȭ���� ��ġ�ϸ�\n������ ������\n������ ���ֺ�����!";

        yield return new WaitForSeconds(3f);
        gameStartText.text = "3";
        yield return new WaitForSeconds(1f);
        gameStartText.text = "2";
        yield return new WaitForSeconds(1f);
        gameStartText.text = "1";
        yield return new WaitForSeconds(1f);
        gameStartText.gameObject.SetActive(false);


        gameObject.GetComponent<Shoot>().enabled = true; // Shoot ��ũ��Ʈ ����
        stopTimer = false; // Ÿ�̸� �۵� ����. false�� Ÿ�̸� ����

    }
}


