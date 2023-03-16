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
        print(playerController.rb.velocity.x);
         if(playerController.rb.velocity.x > 2)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = false;
            print("nigae1");
        }
         else if(playerControll er.rb.velocity.x < -2)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = true;
            print("AEAF2");
        }
        
        {
            animator.SetBool("isRunning", false);
            print("gerer3");
        }
    }
}
