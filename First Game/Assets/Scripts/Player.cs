using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public HungerBar hungerBar;

    Collider2D col;
    Rigidbody2D rb;  

    double halfWidth;
    double halfHeight;

    public float hungerSpeed = 1;
    public float jumpPower = 16;
    public float speed = 10;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<Collider2D>();
        halfHeight = col.bounds.size.y / 2;
        halfWidth = col.bounds.size.x / 2;

        hungerBar.SetMaxHunger(10);
    }

    // Update is called once per frame
    bool IsGrounded()
    {
        //collision on the toes, using the collision and not sticking to walls
        bool isGrounded = Physics2D.OverlapArea(new Vector2((float)(col.bounds.center.x - halfWidth+0.001), (float)(col.bounds.center.y - halfHeight)), new Vector2((float)(col.bounds.center.x + halfWidth-0.001), (float)(col.bounds.center.y - halfHeight + 0.1)), groundLayer);

        return isGrounded;
        
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    void Update()
    {
        Move();
        if (Input.GetButton("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(0, jumpPower);
        }

        hungerBar.SetHunger(hungerBar.slider.value - Time.deltaTime * hungerSpeed);

        //set hungerspeed
        hungerSpeed = 1 + Time.time / 10;   
       
    }

}
