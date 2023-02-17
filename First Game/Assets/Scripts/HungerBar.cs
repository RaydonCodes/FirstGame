using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    // For this type of variable you have to: using UnityEngine.UI;

    public Slider slider;

    public void SetMaxHunger(int maxHunger)
    {
        slider.maxValue = maxHunger;
        slider.value = maxHunger;
    }

    public void SetHunger(float hunger)
    {
        slider.value = hunger;
    }

}
