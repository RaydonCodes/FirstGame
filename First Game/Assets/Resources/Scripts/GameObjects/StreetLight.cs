using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StreetLight : MonoBehaviour
{
    GameObject GlobalLightGameObject;
    Light2D GlobalLight;
    bool TurnedOn = true; // If this isn't true by default then the lamps will still be on even if the Light Intensity > 1 
    void Start()
    {
        GlobalLightGameObject = GameObject.FindWithTag("Global Light");
        GlobalLight = GlobalLightGameObject.GetComponent<Light2D>();
    }
    void Update()
    {
        if((GlobalLight.intensity > 0.35f) && (TurnedOn == true))
        {
            TurnedOn = false;
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        if((GlobalLight.intensity < 0.35f) && (TurnedOn == false))
        {
            TurnedOn = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}