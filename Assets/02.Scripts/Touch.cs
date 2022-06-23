using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour
{
    public GameObject touchText;   // 터치할 시 나오는 Text

    public ParticleSystem eggParticle; // 터치할 시 나오는 Particle

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
                            eggParticle.Play();
                            Status.instance.count++;
                            StartCoroutine(TouchTestText());
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
        touchText.SetActive(true);
        touchText.GetComponent<Text>().text = Status.instance.count.ToString();
        yield return new WaitForSeconds(0.3f);
        touchText.SetActive(false);
    }
}
