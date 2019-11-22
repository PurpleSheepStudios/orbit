using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D myBody;

    public float horizontalThrust;
    
    void Awake()
    {
        horizontalThrust = 1;
        Physics2D.gravity = Vector2.zero;
        myBody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Move();
    }

    void Move()
    {
        myBody.AddForce(new Vector2(horizontalThrust * Input.GetAxisRaw("Horizontal"), 0));
    }
}
