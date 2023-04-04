using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{

    float health = 100;
    float maxHealth = 100;
    [HideInInspector] public bool PlayerIsDead = false;
    public HealthBar healthBar;

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
            Destroy(collision.gameObject)
;        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Food food = other.GetComponent<Food>();
            if (food.canBeEaten)
            {
                // Add food to hunger and destroy food
                AddHealth(food.calories);
                Destroy(other.gameObject);
            }
        }
    }

    public void AddHealth(int healthPoints)
    {
        healthBar.SetHealth(healthBar.slider.value + healthPoints);
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
