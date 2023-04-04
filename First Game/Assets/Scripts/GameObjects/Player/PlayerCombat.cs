using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    PlayerLife pLife;

    [Header("Combat")]
    public float Strength = 5;
    public HealthBar healthBar;
    public GameObject stone;
    public float throwStrength;

    // Start is called before the first frame update
    void Start()
    {
        pLife = gameObject.GetComponent<PlayerLife>();
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
        print(mouseDirection);

        GameObject throwableWeapon = Instantiate(stone, transform.position + Vector3.up * gameObject.transform.localScale.y - Vector3.up * 0.10f, Quaternion.identity);
        Rigidbody2D throwableWeaponRb = throwableWeapon.GetComponent<Rigidbody2D>();
        throwableWeaponRb.AddForce(mouseDirection * throwStrength, ForceMode2D.Impulse);
    }
}