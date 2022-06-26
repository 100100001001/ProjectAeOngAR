using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour
{
    public GameObject touchText;       // 터치할 때 나오는 Text 오브젝트

    public ParticleSystem eggParticle; // 터치할 때 나오는 Particle_Egg
    public ParticleSystem babyParticle; // 터치할 때 나오는 Particle_Baby

    string textAfterTouch;             // 터치 후에 나올 텍스트 string

    public int touchCnt;               // 터치 카운트


    void Update()
    {
        if (Input.touchCount > 0)
        {
            //StartCoroutine(TouchTest());

            //var touch = Input.GetTouch(0);

            //switch (touch.phase)
            //{
            //    case TouchPhase.Began:
            //        touchText.GetComponent<Text>().text = "Began";
            //        break;
            //    case TouchPhase.Moved:
            //        touchText.GetComponent<Text>().text = "Moved";
            //        break;
            //    case TouchPhase.Ended:
            //        touchText.GetComponent<Text>().text = "Ended";
            //        break;
            //}

            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    // 현재 터치 좌표에서 광선 생성
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    RaycastHit hit;

                    // 터치했을 때 나타나는 효과
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "Player")
                        {
                            if (Status.instance.evo == Status.Evolution.EGG) eggParticle.Play();
                            else if (Status.instance.evo == Status.Evolution.BABY) babyParticle.Play();

                            Status.instance.count++;
                            touchCnt++;
                            StartCoroutine(TouchTestText());

                            // 시간 지나면 touchCnt 초기화 -------------------
                        }

                    }
                }
            }

        }
    }

    void ChangeColor()
    {
        transform.GetChild(2).GetComponent<MeshRenderer>().materials.GetValue(1);
    }

    IEnumerator TouchTestText()
    {
        string[] stop = new string[3];
        stop[0] = "이제 괜찮아";
        stop[1] = "...";
        stop[2] = "그만~";

        string[] angry = new string[3];
        stop[0] = "왜 괴롭혀!!!!";
        stop[1] = "너무해!!!!";
        stop[2] = "싫다고!!!!";


        //if (touchCnt < 20) textAfterTouch = Status.instance.count.ToString();
        if (touchCnt < 20) textAfterTouch = "좋아요!";
        else if (touchCnt < 30)
        {
            textAfterTouch = stop[Random.Range(0, 3)];
            Status.instance.evo = Status.Evolution.BABY;
        }
        else if (touchCnt >= 30) textAfterTouch = angry[Random.Range(0, 3)];

        touchText.SetActive(true);
        touchText.GetComponent<Text>().text = touchCnt + " / " + textAfterTouch;
        yield return new WaitForSeconds(0.5f);
        touchText.SetActive(false);
    }
}
