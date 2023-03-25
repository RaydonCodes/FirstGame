using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    Collider2D col;
    
    public float speed;
    int direction = 1;
    LayerMask groundLayer;

    float maxRightXPos = Mathf.NegativeInfinity;
    float maxLeftXPos = Mathf.Infinity;
    bool followPlayer;

    void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        col = gameObject.GetComponent<Collider2D>();
        groundLayer = player.GetComponent<PlayerController>().groundLayer;
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
}
