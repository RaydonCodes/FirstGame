using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    PlatformEffector2D effector;
    GameObject player;


    private void Start()
    {
        effector = gameObject.GetComponent<PlatformEffector2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && player.GetComponent<PlayerController>().rb.velocity.y <= 0.01f)
        {
            effector.rotationalOffset = 180;
            player.GetComponent<PlayerController>().cancelCoyoteTime = true;
        }
        if (player.GetComponent<PlayerController>().hasJumped && player.GetComponent<PlayerController>().cancelCoyoteTime == false)                                                    
        {
            effector.rotationalOffset = 0;
        }
        if ((Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) && player.transform.position.y > gameObject.transform.position.y)
        {
            effector.rotationalOffset = 0;
        }
    }
}
