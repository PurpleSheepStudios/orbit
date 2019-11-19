using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D myBody;

    public float baseMoveSpeed;
    public float acceleration;
    public bool accelerating;
    public long startTime;

    // Use this for initialization
    void Awake()
    {
        baseMoveSpeed = 0.005F;
        acceleration = 1.0F;
        Physics2D.gravity = Vector2.zero;
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        StartAccelerating();
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            myBody.velocity = new Vector2(baseMoveSpeed * acceleration, myBody.velocity.y);
            Accelerate();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            myBody.velocity = new Vector2(-(baseMoveSpeed * acceleration), myBody.velocity.y);
            Accelerate();
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            myBody.velocity = new Vector2(0, myBody.velocity.y);
            accelerating = false;
        }
    }

    void Accelerate()
    {
        DateTime now = DateTime.Now;
        int nowSeconds = now.Millisecond + (now.Second + (now.Minute * 60) + (now.Hour * 3600)) * 1000;
        acceleration = (nowSeconds - startTime);
    }

    void StartAccelerating()
    {
        if (!accelerating)
        {
            DateTime now = DateTime.Now;
            startTime = now.Millisecond + (now.Second + (now.Minute * 60) + (now.Hour * 3600)) * 1000;
        }
        accelerating = true;
    }
}
