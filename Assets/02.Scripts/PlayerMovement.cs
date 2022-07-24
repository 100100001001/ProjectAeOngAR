using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float xMove; // ĳ������ ������ x��
    private float zMove; // ĳ������ ������ z��
    private float moveSpeed = 20f; // ĳ���� �������� ���ǵ尪

    public VirtualJoystick virtualJoyStick; // ���̽�ƽ�� �����ϱ� ���� VirtualJoystick�� �޾ƿ�

    private Rigidbody rigid; // ������ ���� ��ȭ�� ���� Rigidbody�޾ƿ�

    public Animator[] animators; // �����̴� �ִϸ��̼��� �����ϱ� ���� ĳ���͵��� �ִϸ����͸� �޾ƿ´�
    bool isRollAni = false;      // �ִϸ������� ��ȯ ������ ��� bool �Ķ���͸� ����ϱ� ������ ���� ����

    Vector3 savePos; // ĳ���Ͱ� ������ ���� ����Ͽ� ������ ��ġ�� ������ ����

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
            StatusBar.instance.HappyValue(true, 0.1f);

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