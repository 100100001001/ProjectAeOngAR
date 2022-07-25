using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayShootingGame : MonoBehaviour
{
    [Header("--- 슬라이더 ---")]

    [SerializeField]
    Slider timerSlider;        // 남은 시간을 시각화하기 위해 받은 슬라이더

    [SerializeField]
    TextMeshProUGUI timerText; // 남은 시간을 보여줄 텍스트

    [SerializeField]
    Image fillImage;           // 슬라이더 색을 변경하기 위해 이미지와 색상을 받아줌

    public Color32 normalFillColor;  // 슬라이더의 기본 색상
    public Color32 warningFillColor; // 시간이 얼마 안남았을 때 나오는 경고 색상
    public float warningLimit;       // 경고 할 시간을 지정할 변수

    public bool stopTimer;           // 타이머 활성화 유무

    [SerializeField]
    TextMeshProUGUI gameOverText;    // 게임오버시 나올 텍스트

    string textTime;                 // 시간을 알려주기 위한 텍스트

    public float curTime = 20f;      // 슬라이더의 Value값을 조정해주기 위한 시간 변수
    float maxTime = 20f;             // 슬라이더 Value의 최대값

    [Header("--- 게임 시작 ---")]
    public TextMeshProUGUI gameStartText;   // 게임 시작시 나오는 텍스트

    [Header("--- 게임 중 ---")]
    public GameObject[] jellies;            // 젤리 오브젝트들을 담을 변수
    public GameObject targetImage;          // 게임하는 중 화면 중앙에 띄워질 저격 이미지

    [Header("--- 게임 종료 후 ---")]
    public GameObject buttons;              // 다시 시작과 종료하기 버튼이 있는 게임 오브젝트
    public GameObject treasureBox;          // 게임이 끝나면 나올 보물 상자
    public GameObject items;                // 보물 상자를 열면 나오는 아이템
    private Animator ani;                   // 보물 상자의 애니메이션
    public TextMeshProUGUI treasureMessage; // 보물 상자와 관련된 알림 메시지
    public Sprite[] itemSprites;            // 보상 아이템 Sprite

    //// 보류
    //public Texture[] milkTextures;        // 우유 아이템의 색상을 지정해주기 위해 텍스처들을 받아줌
    //public static bool getMilk = false;   // 아이템 획득 여부
    //public static bool getFood = false;   // 아이템 획득 여부
    //public static int milkNumber;         // 아이템 이름 반환
    //public static string foodName;        // 아이템 이름 반환

    void Start()
    {
        // GameOverText 태그를 가지고 있는 게임 오브젝트에서 TextMeshProUGUI 컴포넌트를 가져 옴
        gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TextMeshProUGUI>(); 
        // gameOverText는 게임오버 후 활성화 되어야하기 때문에 비활성화
        gameOverText.gameObject.SetActive(false); 

        // TimerSlider 태그를 가지고 있는 게임 오브젝트에서 Slider 컴포넌트를 가져 옴
        timerSlider = GameObject.FindGameObjectWithTag("TimerSlider").GetComponent<Slider>(); 
        // TimerText 태그를 가지고 있는 게임 오브젝트에서 TextMeshProUGUI 컴포넌트를 가져 옴
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TextMeshProUGUI>(); 
        // SliderFill 태그를 가지고 있는 게임 오브젝트에서 Image 컴포넌트를 가져 옴
        fillImage = GameObject.FindGameObjectWithTag("SliderFill").GetComponent<Image>(); 

        // 보물 상자의 애니메이션
        ani = treasureBox.GetComponent<Animator>(); 

        fillImage.color = normalFillColor;                // 슬라이더의 채움 색을 기본 색상으로 지정해줌
        treasureBox.SetActive(false);                     // 보물 상자 비활성화
        treasureMessage.gameObject.SetActive(false);      // 보물 상자가 나왔을 때의 메시지 비활성화
        items.SetActive(false);                           // 보물 상자가 열리고 나오는 아이템 비활성화
        buttons.SetActive(false);                         // 게임이 끝났을 때 나오는 버튼 비활성화

        stopTimer = true;                                 // 타이머 멈춤 상태
        gameObject.GetComponent<Shoot>().enabled = false; // 총알이 안나오게끔 Shoot 스크립트 비활성화

        foreach (GameObject jelly in jellies) jelly.SetActive(true); // 젤리 활성화

        StartCoroutine(GameStartMessage());                          // 게임 시작 메시지 활성화
    }

    // 게임을 다시 시작할 때마다 활성화
    void OnEnable()
    {
        curTime = 20f;                                       // 슬라이더의 Value값을 조정해주기 위한 시간 변수

        timerSlider.value = (float)curTime / (float)maxTime; // timerSlider의 값을 0~1 사이의 값으로 맞춰주기 위함
        fillImage.color = normalFillColor;                   // 슬라이더의 채움 색을 기본 색상으로 지정해줌

        timerSlider.gameObject.SetActive(true);              // 타이머 슬라이더 오브젝트 활성화
        buttons.SetActive(false);                            // 게임 종료 후 나올 버튼들 비활성화
        treasureBox.SetActive(false);                        // 보물상자 오브젝트 비활성화
        treasureMessage.gameObject.SetActive(false);         // 보물상자가 나왔을 때의 텍스트 비활성화
        items.SetActive(false);                              // 보상 아이템 비활성화
        gameOverText.gameObject.SetActive(false);            // 게임오버 텍스트 비활성화

        Scoring.score = 0;                                   // 점수 초기화

        targetImage.SetActive(true);                         // 화면 중앙에 나올 타깃 이미지 활성화

        foreach (GameObject jelly in jellies) jelly.SetActive(true); // 젤리 활성화

        StartCoroutine(GameStartMessage());                          // 게임 시작 메시지 활성화
    }


    void Update()
    {
        // 남은 시간을 보여주는 텍스트
        textTime = "" + curTime.ToString("f0") + "초 남았어요";

        // 타이머 작동 상태일 때
        if (stopTimer == false)
        {
            curTime -= Time.deltaTime; // 시간을 잰다

            timerText.text = textTime; // 남은 시간 텍스트 업데이트
            timerSlider.value = (float)curTime / (float)maxTime; // 슬라이더의 Value를 계산
        }

        // 시간이 얼마 안 남았을 때 경고
        if (timerSlider.value < ((warningLimit / 100) * timerSlider.maxValue)) 
        {
            fillImage.color = warningFillColor; // 시간이 얼마 안남았을 때 경고 색상으로 활성화
        }

        // 게임오버 상태일 때
        if (curTime <= 0 && stopTimer == false)  
        {
            stopTimer = true;                                 // stopTimer를 true로 바꿔서 타이머 작동을 멈춤
            gameOverText.gameObject.SetActive(true);          // 게임오버 텍스트 활성화
            gameObject.GetComponent<Shoot>().enabled = false; // 총알이 나오지 않게 하기 위해 Shoot 스크립트 비활성화
            timerSlider.gameObject.SetActive(false);          // 타이머 슬라이더 오브젝트 비활성화
            targetImage.SetActive(false);                     // 타깃 이미지 비활성화
            Treasure();                                       // 보물상자가 나오는 메서드 실행
        }
    }

    // 게임 후 보상을 활성화하는 메서드
    void Treasure()
    {
        treasureBox.SetActive(true);                // 보물상자 오브젝트 활성화
        treasureMessage.text = "두구두구두구";      // 보물상자 텍스트 변경
        treasureMessage.gameObject.SetActive(true); // 상자 열기 전 메시지 활성화
        StartCoroutine(ItemsActive());              // 보상 아이템 실행
    }

    // 보상 아이템 실행
    IEnumerator ItemsActive()
    {
        yield return new WaitForSeconds(1f);   // 보물상자 오브젝트가 활성화되고 1초 기다림
        ani.SetTrigger("treasure");            // 보물상자가 열리는 애니메이션 재생
        yield return new WaitForSeconds(0.5f); // 애니메이션이 재생되고 0.5초 기다림
        treasureBox.transform.GetChild(4).gameObject.SetActive(true); // 보물상자가 열릴 때 나오는 폭죽 파티클 오브젝트 활성화

        // 게임 후 점수가 1000점 이상, 또는 1/3의 확률로 캐릭터의 상태값 상승 아이템을 보상으로 받음
        if (Scoring.score >= 1000 || Random.Range(0, 3) == 0)
        {
            int accordingToTheScore = (int)Scoring.score / 5;                   // 획득 점수의 1/2값을 int로 변환해서 변수로 받음
            int itemNum = Random.Range(0, itemSprites.Length);                  // 상승할 상태값을 랜덤 지정하기 위해 랜덤한 숫자를 받아줌
            items.GetComponent<SpriteRenderer>().sprite = itemSprites[itemNum]; // items 오브젝트의 sprite 변경

            yield return new WaitForSeconds(0.5f); // 보상 아이템 획득 전 0.5초의 텀을 둠
            items.SetActive(true);                 // 보상 아이템 활성화

            yield return new WaitForSeconds(1f);         // 1초 텀을 둠
            treasureMessage.gameObject.SetActive(false); // 보물상자 메시지 비활성화
            buttons.SetActive(true);                     // 다시하기, 종료 버튼 활성화

            StatusBar.instance.ActiveValue(true, 20f); // active 값 올라감
            StatusBar.instance.EnergyValue(false, 20); // energy 값 내려감

            // 랜덤한 itemNum 값에 따라 캐릭터의 상태를 올려주고, 올려주는 값은 게임 획득 점수의 1/2값임
            if (itemNum == 0) StatusBar.instance.HappyValue(true, accordingToTheScore);
            else if (itemNum == 1) StatusBar.instance.CleanValue(true, accordingToTheScore);
            else if (itemNum == 2) StatusBar.instance.SmartValue(true, accordingToTheScore);
            else if (itemNum == 3) StatusBar.instance.EnergyValue(true, accordingToTheScore);
            else if (itemNum == 4) StatusBar.instance.HungerValue(true, accordingToTheScore);
            else Debug.Log("");
        }

        // 보상 아이템 획득에 실패했을 때
        else
        {
            treasureMessage.text = "또바기가 즐거웠대요~ >.<~"; // 텍스트 메시지
            yield return new WaitForSeconds(3f);                // 3초 텀을 둠
            treasureMessage.gameObject.SetActive(false);        // 보물상자 메시지 비활성화
            buttons.SetActive(true);                            // 다시하기, 종료 버튼 활성화

            StatusBar.instance.ActiveValue(true, 20f); // active 값 올라감
            StatusBar.instance.EnergyValue(false, 20); // energy 값 내려감
        }
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
        stopTimer = false;                               // 타이머 작동 상태. false면 타이머 실행

    }
}


