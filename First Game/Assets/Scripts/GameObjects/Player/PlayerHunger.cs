using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHunger : MonoBehaviour
{
    public float hungerSpeed = 10;
    public HungerBar hungerBar;

    // Start is called before the first frame update
    void Start()
    {
        hungerBar.SetMaxHunger(100);
    }
    // Update is called once per frame
    void Update()
    {
        hungerBar.SetHunger(hungerBar.slider.value - Time.deltaTime * 10 * hungerSpeed);
        hungerSpeed = 1 + Time.time / 10;
    }
        public void AddHunger(int calories)
    {
        hungerBar.SetHunger(hungerBar.slider.value + calories);
    }
        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Food food = other.GetComponent<Food>();
            if (food.canBeEaten)
            {
                // Add food to hunger and destroy food
                AddHunger(food.calories);
                Destroy(other.gameObject);
            }
        }
    }
}