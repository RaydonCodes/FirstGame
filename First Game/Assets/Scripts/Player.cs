using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    public float jumpPower = 16;
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    bool IsGrounded()
    {
        var area = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 1f), new Vector2(transform.position.x + 0.5f, transform.position.y - 1.1f));
        if (area.tag == "Ground")
        {
            return true;
        }
        else
        {
            print(area.tag);
            return false;
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    void Update()
    {
        if (Input.GetButton("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(0, jumpPower);
        }
        Move();
    }
}
