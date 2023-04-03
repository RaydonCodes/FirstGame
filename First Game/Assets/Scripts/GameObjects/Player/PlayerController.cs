using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Collider2D col;
    [HideInInspector] public Rigidbody2D rb;

    double halfWidth;
    double halfHeight;
    float timeOnAir;

    PlayerLife playerLife;

    // Functionality
    bool hasStoppedJumping;
    bool playerInvulnerable;
    [HideInInspector] public bool hasJumped;
    [HideInInspector] public bool cancelCoyoteTime;
    [HideInInspector] public bool cancelMovement;
    Queue<KeyCode> inputBuffer;

    [Header("IsGrounded box collider")]
    public Vector3 boxSize;
    public float horizontalOffset;
    public float verticalOffset;

    [Header("Player movement magnitudes")]
    public float jumpPower = 16;
    public float shortJumpPower = 4;
    public float speed = 10;
    public float coyoteTime = 0.15f;

    [Header("Combat")]
    public GameObject stone;
    public float throwStrength;
      
    [Header("Other")]
    public LayerMask groundLayer;

    void Start()
    {
        // Get player components
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<BoxCollider2D>();
        playerLife = gameObject.GetComponent<PlayerLife>();

        // Variables used later
        halfHeight = col.bounds.size.y / 2;
        halfWidth = col.bounds.size.x / 2;

        // Input buffer for Jump function
        inputBuffer = new Queue<KeyCode>();
    }
    // Gives the player an extra time to jump after the fell (Quality of life)
    bool CoyoteTime()
    {
        if (timeOnAir < coyoteTime && !cancelCoyoteTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Checks if player is touching the ground
    public bool IsGrounded()
    {
        RaycastHit2D col = Physics2D.BoxCast(transform.position + horizontalOffset * Vector3.right, boxSize, 0, -transform.up, verticalOffset, groundLayer);
        if (col && rb.velocity.y <= 0.5f)
        {
            if(col.collider.tag != "Platform")
            {
                cancelCoyoteTime = true;
            }
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
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private void JumpFunctionality()
    {
        // Jumping if pressed space and (player is on ground or coyote time)
        if (Input.GetButtonDown("Jump") && ((CoyoteTime() && !cancelCoyoteTime) || IsGrounded()))
        {
            Jump();
        }
        // If press space add to inputBuffer (if in ground is still pressing space it will jump again, BUT PLAYER CAN'T HOLD SPACE)
        else if (Input.GetButtonDown("Jump"))
        {
            inputBuffer.Enqueue(KeyCode.Space);
        }
        // Input Buffer (Press space before touch ground)
        if (IsGrounded())
        {
            // Funcionality for other objects like platforms
            timeOnAir = 0;
            hasJumped = false;

            if (inputBuffer.Count > 0)
            {
                if (inputBuffer.Peek() == KeyCode.Space)    //Idk how this works, but it does
                {
                    Jump();
                }
            }
        }
        // If not pressing space remove velocity, going up and only do once
        if (!Input.GetButton("Jump") && rb.velocity.y > shortJumpPower && !hasStoppedJumping)
        {
            // Empty buffer so player doesn't jump instantly when touches ground
            EmptyBuffer();
            // Clamp so if player is going slower than shortJumpPower it doesn't add velocity
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, shortJumpPower, 0.8f));
            // Do it only once
            hasStoppedJumping = true;
        }
        // Due to last if statement can only be done once, we check again (don't doubt my methods)
        else if (Input.GetButtonUp("Jump"))
        {
            EmptyBuffer();
        }
    }

    void Jump()
    {
        hasJumped = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        hasStoppedJumping = false;
        cancelCoyoteTime = false;
        EmptyBuffer();
        
    }
    void Update()
    {
        timeOnAir += Time.deltaTime;

        if (inputBuffer == null)
        {
            inputBuffer = new Queue<KeyCode>();
        }

        if(!cancelMovement)
        {
            JumpFunctionality();
            Move();   
        }
        CheckForCorner();

        if (Input.GetMouseButtonDown(0))
        {
            ThrowWeapon();
        }
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
            float increment = 0;
            if (rightRay.collider.tag != "Platform")
            {
                for (int i = 0; i < 50; i++)
                {
                    // Get how much the player has to teleport to the side, so it doesn't collide
                    increment += 0.05f;
                    RaycastHit2D tempRay = Physics2D.Raycast(new Vector2(col.bounds.center.x + col.bounds.size.x / 2 - increment, col.bounds.center.y), Vector2.up, col.bounds.size.y / 2 + 0.3f, groundLayer);
                    if (tempRay.collider == null)
                    {
                        break;
                    }
                }
            }
            // Teleport the player
            transform.position -= new Vector3(increment, 0, 0);
        }
        if (leftRay && !middleRay && rb.velocity.y > 11.5f)    // Right corner
        {
            float increment = 0;
            if (leftRay.collider.tag != "Platform")
            {
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
            }
            // Teleport the player
            transform.position += new Vector3(increment, 0, 0);
        }
    }


    public IEnumerator KnockBack(float direction, float knockbackStrength)
    {
        if (!playerInvulnerable)
        {
        //Temporal variables

        cancelMovement = true;
        playerInvulnerable = true;

        rb.sharedMaterial.friction = 0.5f;
        rb.sharedMaterial = rb.sharedMaterial;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(knockbackStrength * direction, knockbackStrength / 1.5f) * 2, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.3f);
        StartCoroutine(TurnOnPlayerController());
        
        yield return new WaitForSeconds(1f);
        playerInvulnerable = false;

        }
    }

    IEnumerator TurnOnPlayerController()
    {
        bool loop = true;
        while (loop)
        {
            if ((Input.GetAxisRaw("Horizontal") != 0 || rb.velocity.x == 0) && !playerLife.PlayerIsDead)
            {
                cancelMovement = false;
                rb.sharedMaterial.friction = 0;
                rb.sharedMaterial = rb.sharedMaterial;
                loop = false;
            }
            yield return null;
        }

    }

    void ThrowWeapon()
    {
        Vector2 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(gameObject.transform.position);
        mouseDirection = mouseDirection.normalized;
        print(mouseDirection);

        GameObject throwableWeapon = Instantiate(stone, transform.position + Vector3.up * gameObject.transform.localScale.y - Vector3.up * 0.10f, Quaternion.identity);
        Rigidbody2D throwableWeaponRb = throwableWeapon.GetComponent<Rigidbody2D>();
        throwableWeaponRb.AddForce(mouseDirection * throwStrength, ForceMode2D.Impulse);
    }




    void EmptyBuffer()
    {
        if (inputBuffer.Count > 0)
        {
            inputBuffer.Dequeue();
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + horizontalOffset * Vector3.right - transform.up * verticalOffset, boxSize);
    }

}