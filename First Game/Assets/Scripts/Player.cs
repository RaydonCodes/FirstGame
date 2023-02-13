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
        RaycastHit2D groundCheck = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1.2f);
        return groundCheck.collider != null && groundCheck.collider.CompareTag("Ground");
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
