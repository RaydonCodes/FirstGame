using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
   
    Rigidbody2D rb;
    SpriteRenderer sr;
    double halfWidth;
    double halfHeight;
    public float jumpPower = 16;
    public float speed = 10;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        halfWidth = sr.bounds.size.x / 2;
        halfHeight = sr.bounds.size.y / 2;


    }

    // Update is called once per frame
    bool IsGrounded()
    {
        bool isGrounded = Physics2D.OverlapArea(new Vector2((float)(transform.position.x - halfWidth+0.1), (float)(transform.position.y - halfHeight)), new Vector2((float)(transform.position.x + halfWidth-0.1), (float)(transform.position.y - halfHeight + 0.1)), groundLayer);
        
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
        
    }
}
