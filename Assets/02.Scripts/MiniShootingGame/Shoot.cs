using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총알을 생성하여 몬스터를 조준하고 쏘는 스크립트
public class Shoot : MonoBehaviour
{

    public Transform arCamera;     // AR Camera
    public GameObject bulletOb;

    GameObject bullet;

    public float shootForce = 700f;

    
    // 총알의 랜덤 색상을 위한 변수
    private Renderer bulletColor;
    Color color;

    float time;

    void Start()
    {
        bulletColor = bulletOb.GetComponent<Renderer>();
    }


    void Update()
    {
        time += Time.deltaTime;

        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            bulletOb.SetActive(true);
            color = new Color(Random.value, Random.value, Random.value, 1f);
            bulletColor.material.SetColor("_Color", color);

            bullet = Instantiate(bulletOb, arCamera.position, arCamera.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(arCamera.forward * shootForce);

        }

        if (time > 5)
        {
            time = 0;
            Destroy(bullet);
        }
    }
}
