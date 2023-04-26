using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Variables")]
    public float speed = 5;
    public float hp;
    public float damage;
    public float knockbackStrength;


    protected Rigidbody2D rb;
    protected GameObject player;
    protected Rigidbody2D playerRb;
    protected PlayerController playerController;
    protected PlayerLife playerLife;
    protected Collider2D col;

    
   
    protected int direction = 1;
    protected LayerMask groundLayer;
    public bool cancelMovement;

    // Functionality
    protected float knockbackDirection;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRb = player.GetComponent<Rigidbody2D>();
        playerLife = player.GetComponent<PlayerLife>();
        col = gameObject.GetComponent<BoxCollider2D>();

        groundLayer = playerController.groundLayer;

    }

}
