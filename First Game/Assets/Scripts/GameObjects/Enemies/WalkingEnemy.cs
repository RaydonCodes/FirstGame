using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    Rigidbody2D playerRb;
    PlayerController playerController;
    PlayerLife playerLife;
    Collider2D col;

    [Header("Variables")]
    public float speed = 5;
    public float knockbackStrength = 10;
    int direction = 1;
    LayerMask groundLayer;

    float maxRightXPos = Mathf.Infinity;
    float maxLeftXPos = Mathf.NegativeInfinity;
    float knockbackDirection;
    bool followPlayer;
    bool playerInvulnerable;
    bool hasCollidedWithPlayer;

    void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRb = player.GetComponent<Rigidbody2D>();
        playerLife = player.GetComponent<PlayerLife>();
        col = gameObject.GetComponent<Collider2D>();
       
        groundLayer = playerController.groundLayer;
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
        Move();
        CheckForCorner();

        if (hasCollidedWithPlayer && !playerInvulnerable)
        {
            StartCoroutine(KnockBack(knockbackDirection));
        }
    }

    void SetDirection()
    {
        if (player.transform.position.x > gameObject.transform.position.x && followPlayer)
        {
            direction = 1;
        }
        else if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < .2f && followPlayer)
        {
            direction = 0;
        }
        else if (followPlayer)
        {
            direction = -1;
        }
    }


    void CheckForCorner()
    {

        RaycastHit2D leftRay = Physics2D.Raycast(new Vector2(col.bounds.center.x - col.bounds.size.x / 2, col.bounds.center.y), Vector2.down, 10, groundLayer);
        RaycastHit2D rightRay = Physics2D.Raycast(new Vector2(col.bounds.center.x + col.bounds.size.x / 2, col.bounds.center.y), Vector2.down, 10, groundLayer);

        if (rightRay && !leftRay)
        {
            if (rightRay.distance != leftRay.distance)
            {
                maxLeftXPos = transform.position.x;
                followPlayer = false;
                direction = 1;
            }
        }
        if (!rightRay && leftRay)
        {
            if (rightRay.distance != leftRay.distance)
            {
                maxRightXPos = transform.position.x;
                followPlayer = false;
                direction = -1;
            }
        }

        if (player.transform.position.x -  maxLeftXPos > 0 && maxRightXPos - player.transform.position.x > 0)
        {   
            followPlayer = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            hasCollidedWithPlayer = true;
            knockbackDirection = player.transform.position.x - gameObject.transform.position.x;
            if (knockbackDirection >= 0)
            {
                knockbackDirection = 1;
            }
            else
            {
                knockbackDirection = -1;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            hasCollidedWithPlayer = false;
        }
    }

    IEnumerator KnockBack(float direction){

        //Temporal variables
        float initialSpeed = speed;
        float yForce = knockbackStrength/3;
        
        hasCollidedWithPlayer = false;
        playerController.cancelMovement = true;
        playerInvulnerable = true;

        playerRb.sharedMaterial.friction = 0.5f;
        playerRb.AddForce(new Vector2(knockbackStrength * direction, knockbackStrength/1.5f) * 2, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.3f);
        StartCoroutine(TurnOnPlayerController());

        yield return new WaitForSeconds(.5f);
        playerInvulnerable = false;
        speed = initialSpeed;
    }

    IEnumerator TurnOnPlayerController()
    {
        bool loop = true;
        while (loop)
        {
            if((Input.GetAxisRaw("Horizontal") != 0 || playerRb.velocity.x == 0) && !playerLife.PlayerIsDead)
            {
                print("hi");
                playerController.cancelMovement = false;
                playerRb.sharedMaterial.friction = 0f;
                loop = false;
            }   
            yield return null;
        }

    }
}
