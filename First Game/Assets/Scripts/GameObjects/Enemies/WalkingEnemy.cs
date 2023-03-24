using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    int direction = 1;
    GameObject player;

    void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
        Move();
    }

    void SetDirection()
    {
        if(player.transform.position.x > gameObject.transform.position.x)
        {
            direction = 1;
        }
        else if(Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < .2f)
        {
            direction = 0;
        }
        else
        {
            direction = -1;
        }
    }
}
