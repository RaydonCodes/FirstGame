using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    GameObject player;
    Collider2D col;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        col = gameObject.GetComponent<Collider2D>();
    }

    private void Update()
    {

        if (player.transform.position.y < gameObject.transform.position.y - .2f)
        {
            col.isTrigger = true;
        }
        else if (player.transform.position.y > gameObject.transform.position.y)
        {
            col.isTrigger = false;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && player.transform.position.y > gameObject.transform.position.y - .2f && player.GetComponent<PlayerController>().rb.velocity.y <= 0)
        {
            col.isTrigger = true;
            player.GetComponent<PlayerController>().cancelCoyoteTime = true;
        }
    }
}
