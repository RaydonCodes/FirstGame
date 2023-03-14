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

        if ((Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && player.GetComponent<Player>().IsGrounded())
        {
            effector.rotationalOffset = 180;
        }
        if (Input.GetButtonDown("Jump"))
        {
            effector.rotationalOffset = 0;
        }
    }
}
