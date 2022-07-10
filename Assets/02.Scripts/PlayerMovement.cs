using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xMove;
    public float zMove;
    public float moveSpeed = 5f;

    public VirtualJoystick virtualJoyStick;


    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        xMove = virtualJoyStick.horizontal;
        zMove = virtualJoyStick.vertical;

        transform.Translate(xMove * moveSpeed * Time.deltaTime, 0, zMove * moveSpeed * Time.deltaTime);

    }
}
