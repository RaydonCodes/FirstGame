using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerController playerController;
    Animator animator;
    SpriteRenderer spriteRenderer;

    bool isMoving;
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
        StartCoroutine(IsPlayerMoving());
        if (playerController.rb.velocity.x > 2 && isMoving)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = false;
        }
        else if (playerController.rb.velocity.x < -2 && isMoving)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    IEnumerator IsPlayerMoving()
    {
        float playerXPosition = gameObject.transform.position.x;
        yield return new WaitForSeconds(.1f);
        float temporalXPos = gameObject.transform.position.x;

        if (temporalXPos - playerXPosition == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
    
    public void PlayDeathAnimation()
    {
        animator.SetTrigger("death");
    }
}