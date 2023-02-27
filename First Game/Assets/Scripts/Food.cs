using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int calories = 25;
    [HideInInspector] public bool canBeEaten = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy food's rb (for food that spawns from other object, example: trashcan)
        if (collision.gameObject.layer == 3 && gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }   
    }

    public IEnumerator MakeFoodEdible(float time)
    {
        yield return new WaitForSeconds(time);
        canBeEaten = true;
    }


  
}
