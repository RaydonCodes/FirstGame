using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int calories = 25;
    LayerMask groundLayer = 3;
    [HideInInspector] public bool canBeEaten = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy food's rb (for food that spawns from other object, example: trashcan)
        if (collision.gameObject.layer == 3 && gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());

            // There was a bug that they went a bit through the floor, so I did this to tp to floor 
            RaycastHit2D lookForGround = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1 >> groundLayer);          // I DONT KNOW WHY I HAVE TO USE "1 >> groundLayer", but like this only targets that layer
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Round(lookForGround.point.y), 0);
        }
    }
    public IEnumerator MakeFoodEdible(float time)
    {
        yield return new WaitForSeconds(time);
        canBeEaten = true;
    } 
}