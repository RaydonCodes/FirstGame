using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float knockbackStrength;
    bool IsCollidingWithFloor;

    float time;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            IsCollidingWithFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsCollidingWithFloor = false;
    }

    private void Update()
    {
        if (IsCollidingWithFloor)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        if(time > 3)
        {
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
