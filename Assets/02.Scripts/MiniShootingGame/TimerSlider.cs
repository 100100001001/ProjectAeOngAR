using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TimerSlider : MonoBehaviour
{
    [Header("--- 슬라이더 ---")]

    [SerializeField]
    Slider timerSlider; // 남은 시간을 시각화하기 위해 받은 슬라이더

    [SerializeField]
    TextMeshProUGUI timerText; // 남은 시간

    [SerializeField]
    Image fillImage; // 슬라이더 색을 변경하기 위해 이미지와 색상을 받아줌

    public Color32 normalFillColor;  // 슬라이더의 기본 색상
    public Color32 warningFillColor; // 시간이 얼마 안남았을 때 나오는 경고 색상
    public float warningLimit;       // 경고 할 시간을 지정할 변수

    public bool stopTimer;           // 타이머 유무

    [SerializeField]
    TextMeshProUGUI gameOverText; // 게임오버시 나올 텍스트

    string textTime;              // 시간을 알려주기 위한 텍스트

    public float curTime = 20f;   // 슬라이더의 Value값을 조정해주기 위한 시간 변수
    float maxTime = 20f;          // 슬라이더 Value의 최대값

    [Header("--- 게임 시작 ---")]
    public TextMeshProUGUI gameStartText;

    [Header("--- 게임 중 ---")]
    public GameObject[] jellies;
    public GameObject targetImage; // 게임하는 중 화면 중앙에 띄워질 저격 이미지

    [Header("--- 게임 종료 후 ---")]

    public GameObject buttons;     // 다시 시작과 종료하기 버튼이 있는 게임 오브젝트

    public TextMeshProUGUI t;      // Testing!!!!!!!!!

    public GameObject treasureBox; // 게임이 끝나면 나올 보물 상자
    public GameObject items;       // 보물 상자를 열면 나오는 아이템

    public Texture[] milkTextures; // 우유 아이템의 색상을 지정해주기 위해 텍스처들을 받아줌

    private Animator ani;          // 보물 상자의 애니메이션
    int treasureTouchCnt = 0;      // 보물 상자가 터치하면 열릴 수 있도록 터치 카운트를 세어줌
    public TextMeshProUGUI treasureMessage; // 보물 상자와 관련된 알림 메시지


    public static bool getMilk = false; // 아이템 획득 여부
    public static bool getFood = false; // 아이템 획득 여부
    public static int milkNumber;      // 아이템 이름 반환
    public static string foodName;      // 아이템 이름 반환


    void Start()
    {

        gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TextMeshProUGUI>(); // GameOverText 태그를 가지고 있는 게임 오브젝트에서 TextMeshProUGUI 컴포넌트를 가져 옴
        gameOverText.gameObject.SetActive(false); // gameOverText는 게임오버 후 활성화 되어야하기 때문에 비활성화

        timerSlider = GameObject.FindGameObjectWithTag("TimerSlider").GetComponent<Slider>(); // TimerSlider 태그를 가지고 있는 게임 오브젝트에서 Slider 컴포넌트를 가져 옴
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TextMeshProUGUI>(); // TimerText 태그를 가지고 있는 게임 오브젝트에서 TextMeshProUGUI 컴포넌트를 가져 옴

        fillImage = GameObject.FindGameObjectWithTag("SliderFill").GetComponent<Image>(); // SliderFill 태그를 가지고 있는 게임 오브젝트에서 Image 컴포넌트를 가져 옴

        ani = treasureBox.GetComponent<Animator>();

        fillImage.color = normalFillColor; // 슬라이더의 채움 색을 기본 색상으로 지정해줌
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

        curTime = 20f;   // 슬라이더의 Value값을 조정해주기 위한 시간 변수



        timerSlider.value = (float)curTime / (float)maxTime; // timerSlider의 값을 0~1 사이의 값으로 맞춰주기 위함
        fillImage.color = normalFillColor; // 슬라이더의 채움 색을 기본 색상으로 지정해줌


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

        textTime = "" + curTime.ToString("f0") + "초 남았어요";

        if (stopTimer == false) // 타이머 작동 상태일 때
        {
            curTime -= Time.deltaTime; // 시간을 잰다


            timerText.text = textTime;
            timerSlider.value = (float)curTime / (float)maxTime; // 슬라이더의 Value를 계산
        }

        if (timerSlider.value < ((warningLimit / 100) * timerSlider.maxValue)) // 경고 시간 계산
        {
            fillImage.color = warningFillColor;
        }

        if (curTime <= 0 && stopTimer == false)  // 게임오버 상태
        {
            stopTimer = true;
            gameOverText.gameObject.SetActive(true);
            gameObject.GetComponent<Shoot>().enabled = false; // 총알이 나오지 않게 하기 위해 Shoot 스크립트 비활성화
            timerSlider.gameObject.SetActive(false);
            //Destroy(timerSlider.gameObject);

            targetImage.SetActive(false);


            // "Jelly" 태그를 가진 게임오브젝트들을 비활성화해줌
            //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Jelly");
            //foreach (GameObject enemy in enemies)
            //    enemy.SetActive(false);

            //foreach (GameObject jelly in jellies) jelly.SetActive(false);

            Treasure(); // 보물 상자 

        }
    }


    void Treasure()
    {
        treasureBox.SetActive(true); // 보물상자 활성화

        treasureMessage.text = "두구두구두구";
        treasureMessage.gameObject.SetActive(true); // 상자 열기 전 메시지 활성화

        StartCoroutine(ItemsActive());



        //if (Input.touchCount > 0)
        //{
        //    // 현재 터치 좌표에서 광선 생성
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    RaycastHit hit;

        //    // 터치했을 때 나타나는 효과
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

            treasureMessage.text = "와~ 아이템을 얻었어요!";

            yield return new WaitForSeconds(1f);
        }

        else if (n == 1)
        {
            
            getFood = true;


            // 수정해야함~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            yield return new WaitForSeconds(1.5f);
            items.SetActive(true);
            int milkN = Random.Range(0, milkTextures.Length);
            items.GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", milkTextures[milkN]);
            foodName = milkTextures[n].name;

            treasureMessage.text = "와~ 아이템을 얻었어요!";

            yield return new WaitForSeconds(3f);
        }

        else
        {
            treasureMessage.text = "또바기가 즐거웠대요~ >.<~";
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
        gameStartText.text = "화면을 터치하면\n나오는 공으로\n젤리를 없애보세요!";

        yield return new WaitForSeconds(3f);
        gameStartText.text = "3";
        yield return new WaitForSeconds(1f);
        gameStartText.text = "2";
        yield return new WaitForSeconds(1f);
        gameStartText.text = "1";
        yield return new WaitForSeconds(1f);
        gameStartText.gameObject.SetActive(false);


        gameObject.GetComponent<Shoot>().enabled = true; // Shoot 스크립트 실행
        stopTimer = false; // 타이머 작동 상태. false면 타이머 실행

    }
}


