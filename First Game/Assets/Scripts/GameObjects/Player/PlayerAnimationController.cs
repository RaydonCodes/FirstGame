using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerController playerController;
    Animator animator;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (playerController.rb.velocity.x > 2)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = false;
        }
        else if (playerController.rb.velocity.x < -2)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
    public void PlayDeathAnimation()
    {
        animator.SetTrigger("death");
    }
}