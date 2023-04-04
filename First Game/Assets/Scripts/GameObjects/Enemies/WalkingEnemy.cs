using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{

    float maxRightXPos = Mathf.Infinity;
    float maxLeftXPos = Mathf.NegativeInfinity;
    bool followPlayer;
    bool playerInvulnerable;
    bool hasCollidedWithPlayer;

    void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    
    // Update is called once per frame
    void Update()
    {
        SetDirection();
        if (!cancelMovement)
        {
            Move();
        }
        CheckForCorner();

        if (hasCollidedWithPlayer && !playerInvulnerable)
        {
            StartCoroutine(playerController.KnockBackPlayer(knockbackDirection, knockbackStrength));
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

    //Not falling in void
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

    void DamageEnemy(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }


    public IEnumerator KnockBackEnemy(float enemyKnockbackDirection, float knockbackStrength)
    {
        cancelMovement = true;

        rb.sharedMaterial.friction = 0.7f;
        rb.sharedMaterial = rb.sharedMaterial;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(knockbackStrength * enemyKnockbackDirection, knockbackStrength / 1.5f) * 2, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.2f);
        while(rb.velocity.x >= 0.05f)
        {
            yield return null;
        }
        cancelMovement = false;
        rb.sharedMaterial.friction = 0;
        rb.sharedMaterial = rb.sharedMaterial;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Weapon weapon = collision.gameObject.GetComponentInParent<Weapon>();
            DamageEnemy(weapon.damage);
            Destroy(collision.gameObject.transform.parent.gameObject);
            if (hp > 0)
            {
                float enemyKnockbackDirection = gameObject.transform.position.x - player.transform.position.x;
                if (enemyKnockbackDirection >= 0)
                {
                    enemyKnockbackDirection = 1;
                }
                else
                {
                    enemyKnockbackDirection = -1;
                }

                StartCoroutine(KnockBackEnemy(enemyKnockbackDirection, weapon.knockbackStrength));
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
}
