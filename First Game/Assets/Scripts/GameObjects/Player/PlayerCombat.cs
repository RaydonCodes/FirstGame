using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float Strength = 5;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
            
    }
    public void AddHealth(int healthPoints)
    {
        healthBar.SetHealth(healthBar.slider.value + healthPoints);
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
}