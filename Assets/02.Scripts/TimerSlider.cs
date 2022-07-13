using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public bool stopTimer; // Ÿ�̸� ����

    [SerializeField]
    TextMeshProUGUI gameOverText; // ���ӿ����� ���� �ؽ�Ʈ

    string textTime;              // �ð��� �˷��ֱ� ���� �ؽ�Ʈ

    public float curTime = 20f;   // �����̴��� Value���� �������ֱ� ���� �ð� ����
    float maxTime = 20f;          // �����̴� Value�� �ִ밪

    
    [Header("--- ���� �� ---")]
    public GameObject targetImage; // �����ϴ� �� ȭ�� �߾ӿ� ����� ���� �̹���
    
    [Header("--- ���� ���� �� ---")]

    public GameObject buttons; // �ٽ� ���۰� �����ϱ� ��ư�� �ִ� ���� ������Ʈ

    public TextMeshProUGUI t; 

    public GameObject treasureBox; // ������ ������ ���� ���� ����
    public GameObject items;       // ���� ���ڸ� ���� ������ ������

    public Texture[] milkTextures; // ���� �������� ������ �������ֱ� ���� �ؽ�ó���� �޾���

    private Animator ani;          // ���� ������ �ִϸ��̼�
    int treasureTouchCnt = 0;      // ���� ���ڰ� ��ġ�ϸ� ���� �� �ֵ��� ��ġ ī��Ʈ�� ������
    public TextMeshProUGUI treasureMessage; // ���� ���ڿ� ���õ� �˸� �޽���

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
        fillImage.color = normalFillColor; // �����̴��� ä�� ���� �⺻ �������� ��������


        timerSlider.gameObject.SetActive(true);
        buttons.SetActive(false);
        ani = treasureBox.GetComponent<Animator>();
        treasureBox.SetActive(false);
        treasureMessage.gameObject.SetActive(false);
        items.SetActive(false);


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Jelly");
        foreach (GameObject enemy in enemies)
            enemy.SetActive(true);
    }


    void Update()
    {
        //t.text = "Update : " + curTime;

        curTime -= Time.deltaTime; // �ð��� ���
        textTime = "" + curTime.ToString("f0") + "�� ���Ҿ��";

        if (stopTimer == false)
        {
            timerText.text = textTime;
            timerSlider.value = (float)curTime / (float)maxTime; // �����̴��� Value�� ���
        }

        if (timerSlider.value < ((warningLimit / 100) * timerSlider.maxValue))
        {
            fillImage.color = warningFillColor;
        }

        if (curTime <= 0 && stopTimer == false)  // ���ӿ���
        {
            stopTimer = true;
            gameOverText.gameObject.SetActive(true);
            gameObject.GetComponent<Shoot>().enabled = false;
            timerSlider.gameObject.SetActive(false);
            //Destroy(timerSlider.gameObject);

            targetImage.SetActive(false);

            // "Jelly" �±׸� ���� ���ӿ�����Ʈ���� ��Ȱ��ȭ����
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Jelly");
            foreach (GameObject enemy in enemies)
                enemy.SetActive(false);

            Treasure();

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

        treasureMessage.text = "���ڸ� �������~!";
        treasureMessage.gameObject.SetActive(true);


        if (Input.touchCount > 0)
        {
            // ���� ��ġ ��ǥ���� ���� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // ��ġ���� �� ��Ÿ���� ȿ��
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Treasure")
            {

                ani.SetTrigger("treasure");
                StartCoroutine(ItemsActive());

                treasureTouchCnt = 0;


                //treasureTouchCnt++;

                //if (treasureTouchCnt > 5)
                //{
                    

                //}
            }
        }


    }

    IEnumerator ItemsActive()
    {
        yield return new WaitForSeconds(3f);

        items.SetActive(true);
        int n = Random.Range(0, milkTextures.Length);
        items.GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", milkTextures[n]);

        treasureMessage.text = "��~ �������� ������!";

        yield return new WaitForSeconds(3f);
        treasureMessage.gameObject.SetActive(false);
        buttons.SetActive(true);
    }
}


