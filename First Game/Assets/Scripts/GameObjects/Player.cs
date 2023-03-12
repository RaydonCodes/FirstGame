using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    Collider2D col;
    Rigidbody2D rb;  

    double halfWidth;
    double halfHeight;
    bool hasJumped;

    [Header("IsGrounded box collider")]
    public Vector3 boxSize;
    public float horizontalOffset;
    public float verticalOffset;

    [Header("Player movement magnitudes")]
    public float hungerSpeed = 10;
    public float jumpPower = 16;
    public float speed = 10;
    public float coyoteTime = 0.15f;

    float timeOnAir;
    
    [Header("Other")]
    public LayerMask groundLayer;
    public HungerBar hungerBar;

    void Start()
    {                            
        // Get player components
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<BoxCollider2D>();

        // Variables used later
        halfHeight = col.bounds.size.y / 2;
        halfWidth = col.bounds.size.x / 2;

        hungerBar.SetMaxHunger(100);
    }
    // Gives the player an extra time to jump after the fell (Quality of life)
    void CoyoteTime()
    {
        timeOnAir += Time.deltaTime;
        if (IsGrounded())
        {
            hasJumped = false;
            timeOnAir = 0;
        }
    }
    // Checks if player is touching the ground
    bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position + horizontalOffset * Vector3.right, boxSize, 0, -transform.up, verticalOffset, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime * 40, rb.velocity.y);
    }
    void Update()
    {

        hungerBar.SetHunger(hungerBar.slider.value - Time.deltaTime * 10 * hungerSpeed);
        
        // Set hungerspeed
        hungerSpeed = 1 + Time.time / 10;

    }
    void CheckForCorner()
    {
        // This is a gameplay improvement function, it checks if the player hits barely a corner when jumping, if so it teleports the player so he doesn't collide with it

        RaycastHit2D leftRay = Physics2D.Raycast(new Vector2(col.bounds.center.x - col.bounds.size.x / 2, col.bounds.center.y), Vector2.up, col.bounds.size.y / 2 + 0.3f, groundLayer);
        RaycastHit2D rightRay = Physics2D.Raycast(new Vector2(col.bounds.center.x + col.bounds.size.x / 2, col.bounds.center.y), Vector2.up, col.bounds.size.y / 2 + 0.3f, groundLayer);
        RaycastHit2D middleRay = Physics2D.Raycast(new Vector2(col.bounds.center.x, col.bounds.center.y), Vector2.up, col.bounds.size.y / 2 + 0.3f, groundLayer);
        
        // Check if player its a corner, and is also jumping (y velocity is high)
        if (rightRay && !middleRay && rb.velocity.y > 11.5f)    // Left corner
        {
            print("NIGGNAIGNAIGNEINAIGNIE");
            float increment = 0;
            for(int i = 0; i < 50; i++)
            {
                // Get how much the player has to teleport to the side, so it doesn't collide
                increment += 0.05f;
                RaycastHit2D tempRay = Physics2D.Raycast(new Vector2(col.bounds.center.x + col.bounds.size.x / 2 - increment, col.bounds.center.y), Vector2.up, col.bounds.size.y / 2 + 0.3f, groundLayer);
                if (tempRay.collider == null)
                {
                    break;
                }
            }
            // Teleport the player
            transform.position -= new Vector3(increment, 0, 0);
        }
        if (leftRay && !middleRay && rb.velocity.y > 11.5f)    // Right corner
        {
            print("NIGGNAIGNAIGNEINAIGNIE");
            float increment = 0;
            for (int i = 0; i < 50; i++)
            {
                // Get how much the player has to teleport to the side, so it doesn't collide
                increment += 0.05f;
                RaycastHit2D tempRay = Physics2D.Raycast(new Vector2(col.bounds.center.x - col.bounds.size.x / 2 + increment, col.bounds.center.y), Vector2.up, col.bounds.size.y / 2 + 0.3f, groundLayer);
                if (tempRay.collider == null)
                {
                    break;
                }
            }
            // Teleport the player
            transform.position += new Vector3(increment, 0, 0);
        }
    }
    private void FixedUpdate()      // FixedUpdate is used for physics
    {
        if (Input.GetButton("Jump") && timeOnAir < coyoteTime && !hasJumped)
        {
            hasJumped = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower) * Time.deltaTime * 50;
        }
        Move();
        CoyoteTime();
        CheckForCorner();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {            
        if (other.tag == "Food")
        {
            Food food = other.GetComponent<Food>();
            if (food.canBeEaten)
            {
                // Add food to hunger and destroy food
                hungerBar.SetHunger(hungerBar.slider.value + food.calories);
                Destroy(other.gameObject);
            }
        }      
        if (other.tag == "Chest")
        {
            ContainerOpener container = other.gameObject.GetComponent<ContainerOpener>();
            if (!container.hasBeenOpened)
            {
                // Open the trashcan
                StartCoroutine(container.OpenChest());
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + horizontalOffset * Vector3.right - transform.up * verticalOffset, boxSize);  
    }
}