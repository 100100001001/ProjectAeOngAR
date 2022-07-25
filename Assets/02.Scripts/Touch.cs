using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Touch : MonoBehaviour
{

    [Header("--- 말풍선 ---")]
    public GameObject bubble;            // 말풍선 오브젝트를 담아두는 변수
    private RectTransform bubbleRT;      // 말풍선 위치가 랜덤으로 뜨기 위해 RectTransform을 받아오는 변수
    private Image bubbleImg;             // 말풍선 오브젝트의 Image를 불러오기 위한 변수

    public Sprite[] bubbleSprites;       // 말풍선 sprite들을 담아두는 변수

    private TextMeshProUGUI bubbleTMPro; // TextMeshPro를 담아두기 위한 변수
    private string bubbleText;           // TextMeshPro의 텍스트를 변경하기 위한 변수


    [Header("--- 파티클 ---")]
    public ParticleSystem eggParticle;   // 터치할 때 나오는 Particle_Egg
    public ParticleSystem babyParticle;  // 터치할 때 나오는 Particle_Baby
    public ParticleSystem childParticle; // 터치할 때 나오는 Particle_Child
    public ParticleSystem youthParticle; // 터치할 때 나오는 Particle_Youth



    [Header("--- 터치 애니메이션 ---")]
    public GameObject players;
    private Animator[] animator;  // 사용할 애니메이터 컴포넌트

    public GameObject egg;
    public GameObject baby;
    public GameObject child;
    public GameObject youth;


    public int limitTouchCnt;        // 임시 터치 카운트 (시간이 지남에 따라 초기화 될 수 있는 단발성 터치 카운트)

    public TextMeshProUGUI testTest;

    float time;




    void Start()
    {
        bubbleRT = bubble.GetComponent<RectTransform>();
        bubbleImg = bubble.GetComponent<Image>();

        bubbleTMPro = bubble.GetComponentInChildren<TextMeshProUGUI>();

        animator = players.GetComponentsInChildren<Animator>();
    }

    void Update()
    {
        time += Time.deltaTime;

        // 화면 터치가 1회 이상일 때 실행
        if (Input.touchCount > 0)
        {
            // 현재 터치 좌표에서 광선 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // 터치했을 때 나타나는 효과
            // 광선의 충돌체 태그가 "Player(또바기)"일 때 실행
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Player")
            {
                // 또바기를 2회 이상 터치했을 때
                if (Input.touchCount > 1)
                {
                    // 싫어하는 말풍선이 화면에 띄워지며 또바기의 Happy정도를 감소시킴
                    StartCoroutine(ThinkingBubble("many_touches"));
                    StatusBar.instance.HappyValue(false, 5);
                }

                // 터치를 시작했을 때
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    // limitTouchCnt를 올려주고 터치 후 반응 메서드 실행
                    limitTouchCnt++;
                    TouchResponse();

                    // 1분이 지나면 touchCnt 초기화
                    if (time > 60)
                    {
                        limitTouchCnt = 0;
                        time = 0;
                    }
                }
            }
        }

        // 원본 업데이트 (터치 실험 중)
        //void Update()
        //{
        //    //if (Input.GetKeyDown(KeyCode.Space)) StatusBar.instance.HappyValue(true);

        //    if (Input.touchCount > 0)
        //    {
        //        //StartCoroutine(TouchTest());

        //        //var touch = Input.GetTouch(0);

        //        //switch (touch.phase)
        //        //{
        //        //    case TouchPhase.Began:
        //        //        touchText.GetComponent<Text>().text = "Began";
        //        //        break;
        //        //    case TouchPhase.Moved:
        //        //        touchText.GetComponent<Text>().text = "Moved";
        //        //        break;
        //        //    case TouchPhase.Ended:
        //        //        touchText.GetComponent<Text>().text = "Ended";
        //        //        break;
        //        //}

        //        for (int i = 0; i < Input.touchCount; ++i)
        //        {

        //            if (Input.GetTouch(i).phase == TouchPhase.Began)
        //            {
        //                // 현재 터치 좌표에서 광선 생성
        //                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
        //                RaycastHit hit;

        //                // 터치했을 때 나타나는 효과
        //                if (Physics.Raycast(ray, out hit))
        //                {
        //                    if (hit.transform.tag == "Player")
        //                    {
        //                        if (Status.instance.evo1 == Status.Evolution1.EGG) eggParticle.Play();
        //                        else if (Status.instance.evo1 == Status.Evolution1.BABY) babyParticle.Play();
        //                        else if (Status.instance.evo1 == Status.Evolution1.CHILD) childParticle.Play();
        //                        else if (Status.instance.evo1 == Status.Evolution1.YOUTH) youthParticle.Play();

        //                        Status.instance.cntTouch1++;

        //                        touchCnt++;
        //                        TouchResponse();

        //                        // 시간 지나면 touchCnt 초기화 -------------------
        //                    }

        //                }
        //            }



        //            //if (Input.GetTouch(i).phase == TouchPhase.Moved)
        //            //{
        //            //    if (Status.instance.evo1 == Status.Evolution1.EGG)
        //            //    {
        //            //        animator[0].SetBool("isRoll", true);


        //            //        egg.transform.Translate(Input.GetTouch(i).deltaPosition * Time.deltaTime * 2f);
        //            //        baby.transform.Translate(Input.GetTouch(i).deltaPosition * Time.deltaTime * 2f);
        //            //        child.transform.Translate(Input.GetTouch(i).deltaPosition * Time.deltaTime * 2f);
        //            //        youth.transform.Translate(Input.GetTouch(i).deltaPosition * Time.deltaTime * 2f);
        //            //    }



        //            //}

        //            //else if (Input.GetTouch(i).phase == TouchPhase.Ended)
        //            //{
        //            //    if (Status.instance.evo1 == Status.Evolution1.EGG)
        //            //    {
        //            //        animator[0].SetBool("isRoll", false);
        //            //    }

        //            //}

        //        }

        //    }
        //}


        // 터치했을 때 반응하는 메서드
        void TouchResponse()
        {
            // Egg상태일 때 나오는 말풍선
            if (Status.instance.evo1 == Status.Evolution1.EGG) StartCoroutine(RecBubble("eggHappy"));

            //// limitTouchCnt에 따라 다른 반응이 나오도록 if문으로 나눠놓음
            // 터치 했을 때 Happy값을 올리고 좋아하는 말풍선과 파티클이 나오도록 함
            if (limitTouchCnt < 30)
            {
                StatusBar.instance.HappyValue(true, 5);
                StartCoroutine(RecBubble("happy"));
                PlayTouchParticle();
            }

            // 터치를 많이 할수록 싫어하기 때문에, 그만하라는 메시지를 띄움
            else if (limitTouchCnt < 40)
            {
                StartCoroutine(ThinkingBubble("stop"));
            }

            // 그만하라는 메시지를 넘어갔을 때 싫어하는 말풍선을 띄우고 Happy값을 내려줌
            else if (limitTouchCnt >= 40)
            {
                StartCoroutine(ThinkingBubble("angry"));
                StatusBar.instance.HappyValue(false, 5);
            }
        }

        void PlayTouchParticle()
        {
            if (Status.instance.evo1 == Status.Evolution1.EGG) eggParticle.Play();
            else if (Status.instance.evo1 == Status.Evolution1.BABY) babyParticle.Play();
            else if (Status.instance.evo1 == Status.Evolution1.CHILD) childParticle.Play();
            else if (Status.instance.evo1 == Status.Evolution1.YOUTH) youthParticle.Play();
        }

        /// <summary>
        /// 반려동물(캐릭터)가 생각하는 말풍선
        /// </summary>
        /// <param name="st">상태에 따라 달라지는 말풍선 텍스트를 구분하기 위한 매개변수</param>
        /// <returns></returns>
        IEnumerator ThinkingBubble(string st)
        {
            // 화난 텍스트
            if (st == "angry")
            { 
                string[] thinkingText = new string[5];
                thinkingText[0] = "그만 하라고 했는데?";
                thinkingText[1] = "왜 괴롭히지?";
                thinkingText[2] = "뭐야 그만하지;";
                thinkingText[3] = "그만해라";
                thinkingText[4] = "그만하라고";

                // 화난 텍스트 문구 중 랜덤 지정
                bubbleText = thinkingText[Random.Range(0, 5)];
            
            }

            // 많은 터치를 했을 때
            else if (st == "many_touches")
            {
                string[] thinkingText = new string[3];
                thinkingText[0] = "날 소중히\n대해라";
                thinkingText[1] = "많이 터치하면\n아픈디..";
                thinkingText[2] = "한손으로만\n터치해줘..";
                thinkingText[3] = "한손으로만...\n톡...\n만져주지...";
                thinkingText[4] = "악!! 막\n만지면 아파";

                // 텍스트 문구 중 랜덤 지정
                bubbleText = thinkingText[Random.Range(0, 5)];
            }

            // 말풍선 오브젝트 활성화
            bubble.SetActive(true);
            // 말풍선 이미지를 생각하는 sprite로 변경
            bubbleImg.sprite = bubbleSprites[0];
            // 말풍선의 위치는 랜덤 지정
            bubbleRT.anchoredPosition = new Vector3(Random.Range(-310, 260), Random.Range(-170, 100), 0);
            // 말풍선 텍스트 변경
            bubbleTMPro.text = bubbleText;
            // 말풍선은 3초 후에 비활성화되로록 함
            yield return new WaitForSeconds(3f);
            bubble.SetActive(false);
        }

        /// <summary>
        /// 반려동물(캐릭터)의 말풍선
        /// </summary>
        /// <param name="st">상태에 따라 달라지는 말풍선 텍스트를 구분하기 위한 매개변수</param>
        /// <returns></returns>
        IEnumerator RecBubble(string st)
        {
            if (st == "stop")
            {

                string[] thinkingText = new string[5];
                thinkingText[0] = "이제 괜찮아";
                thinkingText[1] = "...";
                thinkingText[2] = "그만~";
                thinkingText[3] = "이제 그만해줘~!";
                thinkingText[4] = "이제 하지마~";

                bubbleText = thinkingText[Random.Range(0, 5)];
            }

            else if (st == "happy")
            {

                string[] thinkingText = new string[6];
                thinkingText[0] = "히히\n좋아";
                thinkingText[1] = "기분\n좋아!!!";
                thinkingText[2] = "더 쓰다듬어줘!";
                thinkingText[3] = "찰떡같이\n쓰다듬네";
                thinkingText[4] = "꺄악 신나!!!!";
                thinkingText[5] = "행복해~!!";

                bubbleText = thinkingText[Random.Range(0, 6)];

            }

            else if (st == "eggHappy")
            {

                string[] thinkingText = new string[2];
                thinkingText[0] = "♡";
                thinkingText[0] = "♥";

            }

            bubble.SetActive(true);

            bubbleImg.sprite = bubbleSprites[1];
            bubbleRT.anchoredPosition = new Vector3(Random.Range(-310, 260), Random.Range(-170, 100), 0);

            bubbleTMPro.text = bubbleText;

            yield return new WaitForSeconds(3f);
            bubble.SetActive(false);
        }

    }
}


