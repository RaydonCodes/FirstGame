using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    PlayerLife pLife;
    Rigidbody2D rb;
    PlayerController pController;

    [Header("Combat")]
    public float Strength = 5;
    public HealthBar healthBar;
    public GameObject stone;
    public float throwStrength;
    public float throwRotationStrength;

    // Start is called before the first frame update
    void Start()
    {
        pLife = gameObject.GetComponent<PlayerLife>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        pController = gameObject.GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowWeapon();
        }
               
    } 

    void ThrowWeapon()
    {
        Vector2 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(gameObject.transform.position);
        mouseDirection = mouseDirection.normalized;
        float rotateDir;
        if(mouseDirection.x >= 0)
        {
            rotateDir = -1;
        }
        else
        {
            rotateDir = 1;
        }

        GameObject throwableWeapon = Instantiate(stone, transform.position + Vector3.up * 2, Quaternion.identity);
        Rigidbody2D throwableWeaponRb = throwableWeapon.GetComponent<Rigidbody2D>();
        throwableWeaponRb.AddForce(Vector2.right * rb.velocity.x + mouseDirection * throwStrength + Vector2.up * .5f, ForceMode2D.Impulse);
        throwableWeaponRb.AddTorque(rotateDir * throwRotationStrength);
    }
}