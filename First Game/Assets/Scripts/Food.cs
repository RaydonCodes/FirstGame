using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int calories = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }   
    }
  
}
