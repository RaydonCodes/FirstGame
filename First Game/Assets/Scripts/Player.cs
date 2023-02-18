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
    bool hasJumped;

    public float hungerSpeed = 10;
    public float jumpPower = 16;
    public float speed = 10;
    float timeOnAir;
    public LayerMask groundLayer;


    //raycasts
    RaycastHit2D rightRay;
    RaycastHit2D leftRay;

    // Start is called before the first frame update
    void Start()
    {                               
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<Collider2D>();
        halfHeight = col.bounds.size.y / 2;
        halfWidth = col.bounds.size.x / 2;


        hungerBar.SetMaxHunger(100);
    }

    // Update is called once per frame
    bool IsGrounded()
    {
        //collision on the toes, using the collision and not sticking to walls
        Vector2 point1 = new Vector2((float)(col.bounds.center.x - halfWidth + 0.01), (float)(col.bounds.center.y - halfHeight));
        Vector2 point2 = new Vector2((float)(col.bounds.center.x + halfWidth - 0.01), (float)(col.bounds.center.y - halfHeight + 0.1)); 

        bool isGrounded = Physics2D.OverlapArea(point1, point2, groundLayer);

        return isGrounded;       
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime * 20, rb.velocity.y);
    }

    void Update()
    {
        rightRay = Physics2D.Raycast(new Vector2(col.bounds.center.x - col.bounds.size.x / 2 + 0.01f, col.bounds.center.y), Vector2.up, col.bounds.size.y / 2 + 0.4f, groundLayer);
        leftRay = Physics2D.Raycast(new Vector2(col.bounds.center.x + col.bounds.size.x / 2 - 0.01f, col.bounds.center.y), Vector2.up, col.bounds.size.y / 2 + 0.4f, groundLayer);

        if (rightRay && !leftRay)
        {
            bool loop = true;
            float increment = 0;
            while (loop)
            {
                increment += 0.2f;
                RaycastHit2D tempRay = Physics2D.Raycast(new Vector2(col.bounds.center.x - col.bounds.size.x / 2 + 0.01f + increment, col.bounds.center.y), Vector2.up, col.bounds.size.y / 2 + 0.4f, groundLayer);
                if (tempRay.collider == null)
                {
                    loop = false;
                }
            }
            transform.position += new Vector3(increment, 0, 0);
        }
        if (leftRay && !rightRay)
        {
            bool loop = true;
            float increment = 0;
            while (loop)
            {
                increment += 0.2f;
                RaycastHit2D tempRay = Physics2D.Raycast(new Vector2(col.bounds.center.x + col.bounds.size.x / 2 - 0.01f - increment, col.bounds.center.y), Vector2.up, col.bounds.size.y / 2 + 0.4f, groundLayer);
                if (tempRay.collider == null)
                {
                    loop = false;
                }
            }
            transform.position -= new Vector3(increment, 0, 0);
        }

        hungerBar.SetHunger(hungerBar.slider.value - Time.deltaTime * 10 * hungerSpeed);
        // Set hungerspeed
        hungerSpeed = 1 + Time.time / 10;

        /*    VISUALIZATION OF rightRay and leftRay
        
        Debug.DrawLine(new Vector2(col.bounds.center.x - col.bounds.size.x / 2, col.bounds.center.y), new Vector2(col.bounds.center.x - col.bounds.size.x / 2, col.bounds.center.y) + new Vector2(0, col.bounds.size.y / 2 + 0.1f));
        Debug.DrawLine(new Vector2(col.bounds.center.x + col.bounds.size.x / 2, col.bounds.center.y), new Vector2(col.bounds.center.x + col.bounds.size.x / 2, col.bounds.center.y) + new Vector2(0, col.bounds.size.y / 2 + 0.1f));

        */
    }


    private void FixedUpdate()
    {
        timeOnAir += Time.deltaTime;
        if (IsGrounded())
        {
            hasJumped = false;
            timeOnAir = 0;
        }
        Move();
        if (Input.GetButton("Jump") && timeOnAir < 0.15 && !hasJumped)
        {
            hasJumped = true;


            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Food food = other.GetComponent<Food>();
            hungerBar.SetHunger(hungerBar.slider.value + food.calories);
            Destroy(other.gameObject);
        }
    }
}