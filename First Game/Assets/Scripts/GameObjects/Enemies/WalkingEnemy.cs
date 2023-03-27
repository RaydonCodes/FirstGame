using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    Collider2D col;
    PlayerController playerController;

    [Header("Variables")]
    public float speed = 5;
    public float knockBackTime = 0.3;
    public float knockbackStrength = 1;
    int direction = 1;
    LayerMask groundLayer;

    float maxRightXPos = Mathf.NegativeInfinity;
    float maxLeftXPos = Mathf.Infinity;
    bool followPlayer;
    bool playerInvulnerable;

    void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        col = gameObject.GetComponent<Collider2D>();
        groundLayer = playerController.groundLayer;
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
        Move();
        CheckForCorner();
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

        print(player.transform.position.x - maxLeftXPos);
        print(maxRightXPos - player.transform.position.x);

        if (player.transform.position.x -  maxLeftXPos > 0 && maxRightXPos - player.transform.position.x > 0)
        {   
            followPlayer = true;
        }
        print(followPlayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            float direction = gameObject.transform.position.x - player.transform.position.x;
            if (direction >= 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            print("balls");
            float strength = 10;
            rb = player.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.right * strength * direction, ForceMode2D.Impulse);
        }

    IEnumerator KnockBack(int direction){
        
        //Temporal variables
        float initialKnockbackStrength = knockbackStrength;

        float yForce = knockbackStrength/3;
        playerController.enabled = false;
        rb.velocity = new Vector2(knockbackStrength * direction, yForce);
        playerInvulnerable = true;

        while(knockbackStrength > 0){
            
            float deltaTimeMultiplier = knockbackStrength / knockBackTime;

            knockbackStrength -= Time.deltaTime * deltaTimeMultiplier;
            yForce -= Time.deltaTime * deltaTimeMultiplier/3;
            rb.velocity = new Vector2(knockbackStrength * direction, yForce);
            if (knockbackStrength < 0){
                knockbackStrength = 0;
            }
            yield return null;
        }
        playerController.enabled = true;
        knockbackStrength = initialKnockbackStrength;
        yield return new WaitForSeconds(1f);
        playerInvulnerable = false;
        
    }
}
