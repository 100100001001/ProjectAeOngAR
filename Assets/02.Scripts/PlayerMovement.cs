using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float xMove;
    private float zMove;
    private float moveSpeed = 20f;

    public VirtualJoystick virtualJoyStick;

    private Rigidbody rigid;


    private Animator[] animators;

    bool isRollAni = false;

    Vector3 testPos;

    float time;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animators = GetComponentsInChildren<Animator>();

        testPos = transform.position;

    }

    void Update()
    {
        time += Time.deltaTime;


        if (time > 5)
        {
            time = 0;
            testPos = transform.position;
            Debug.Log("=========="+testPos);
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

        //transform.Translate(xMove * moveSpeed * Time.deltaTime, 0, zMove * moveSpeed * Time.deltaTime);
        //transform.position += new Vector3(xMove * moveSpeed * Time.deltaTime, 0, zMove * moveSpeed * Time.deltaTime);

        rigid.velocity = new Vector3(xMove * moveSpeed * Time.deltaTime, 0, zMove * moveSpeed * Time.deltaTime);

        if (rigid.velocity != Vector3.zero)
        {
            isRollAni = true;
            StatusBar.instance.ActiveValue(true, 0.05f);
            Debug.Log(StatusBar.instance.curActive);
        }


        foreach (Animator a in animators)
        {
            a.SetBool("isRoll", isRollAni);
        }
        Debug.Log(rigid.velocity);


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fall")
        {
            Debug.Log("‰Ñ´Ï?");
            transform.position = testPos;
        }
    }

}