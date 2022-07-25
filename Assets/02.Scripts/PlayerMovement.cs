using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float xMove; // 캐릭터의 움직임 x값
    private float zMove; // 캐릭터의 움직임 z값
    private float moveSpeed = 20f; // 캐릭터 움직임의 스피드값

    public VirtualJoystick virtualJoyStick; // 조이스틱과 연결하기 위해 VirtualJoystick을 받아옴

    private Rigidbody rigid;     // 움직임 값의 변화를 위해 Rigidbody받아옴

    public Animator[] animators; // 움직이는 애니메이션을 실행하기 위해 캐릭터들의 애니메이터를 받아온다
    bool isRollAni = false;      // 애니메이터의 전환 조건은 모두 bool 파라미터를 사용하기 때문에 변수 선언

    Vector3 savePos;             // 캐릭터가 떨어질 것을 대비하여 본래의 위치를 저장할 변수

    float time;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        savePos = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime;


        if (time > 5)
        {
            time = 0;
            savePos = transform.position;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        isRollAni = false;

        xMove = virtualJoyStick.horizontal;
        zMove = virtualJoyStick.vertical;

        rigid.velocity = new Vector3(xMove * moveSpeed * Time.deltaTime, 0, zMove * moveSpeed * Time.deltaTime);

        if (rigid.velocity != Vector3.zero)
        {
            isRollAni = true;
            StatusBar.instance.ActiveValue(true, 0.1f);
        }

        foreach (Animator a in animators)
        {
            a.SetBool("isRoll", isRollAni);
        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fall")
        {
            transform.position = savePos;
        }
    }

}