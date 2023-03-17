using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{

    float health = 100;
    float maxHealth = 100;

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
        print(health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.GetComponent<PlayerAnimationController>().PlayDeathAnimation();
    }
}
