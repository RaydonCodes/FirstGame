using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
public class StreetLight : MonoBehaviour
{
    Volume backgroundLightGameObject;
    Light2D BackgroundLight;
    bool TurnedOn = true; // If this isn't true by default then the lamps will still be on even if the Light Intensity > 1 
    void Start()
    {
        backgroundLightGameObject = GameObject.Find("Global Volume").GetComponent<Volume>();
    }
    void Update()
    {
        if((backgroundLightGameObject.weight < 0.70f) && (TurnedOn == true))
        {
            TurnedOn = false;
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        if((backgroundLightGameObject.weight > 0.70f) && (TurnedOn == false))
        {
            TurnedOn = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}