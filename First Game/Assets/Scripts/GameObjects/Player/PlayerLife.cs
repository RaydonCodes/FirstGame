using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{

    float health = 100;
    float maxHealth = 100;
    [HideInInspector] public bool PlayerIsDead = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Harmful")
        {
            DamagePlayer(collision.gameObject.GetComponent<Harmful>().damagePoints);
        }
    }

    void DamagePlayer(int damagePoints)
    {
        health -= damagePoints;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        RaycastHit2D dieCast = Physics2D.Raycast(gameObject.transform.position, Vector2.down, playerController.groundLayer);
        PlayerIsDead = true;
        gameObject.transform.position = (dieCast.point);
        playerController.enabled = false;
        gameObject.GetComponent<PlayerAnimationController>().PlayDeathAnimation();
    }
}
