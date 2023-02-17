using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    //divided by 10 after
    public int calories = 25;

    private void OnCollisionEnter(Collision collision)
    {

        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            HungerBar hungerbar = player.hungerBar;
            hungerbar.SetHunger(hungerbar.slider.value + calories / 10);
        }  
    }

}
