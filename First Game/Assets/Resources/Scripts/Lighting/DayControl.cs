using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;


public class DayControl : MonoBehaviour
{
    public int dayLengthSecs;      
    public int nightLengthSecs;
    float timeMultiplier;
    int cicleLength;
    float totalSecondsPassed;
    float seconds;
    float minutes;
    float hours;
    float days;         
    float currentIntensity;
    GameObject volume;
    // Start is called before the first frame update
    void Start()
    {
        volume = GameObject.Find("Global Volume");
        cicleLength = dayLengthSecs + nightLengthSecs;
        timeMultiplier = 86400 / cicleLength;
        print(timeMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime * timeMultiplier;
        totalSecondsPassed += Time.deltaTime * timeMultiplier;
        if (seconds >= 60)
        {
            seconds -= 60;
            minutes += 1;
        }
        if(minutes >= 60)
        {
            minutes -= 60;
            hours += 1;
        }
        if(hours >= 24)
        {
            hours -= 24;
            days += 1;
        }
        DayIntensity();
        // print("seconds: " + seconds + " min: " + minutes + " hours: " + hours + " days: " + days);
    }

    void DayIntensity()
    {
        if (totalSecondsPassed % 86400 > dayLengthSecs * timeMultiplier & currentIntensity < 1) 
        {
            currentIntensity += Time.deltaTime / (cicleLength / 8);
            print("night");
        }
        else if(totalSecondsPassed % 86400 < dayLengthSecs * timeMultiplier & currentIntensity > 0)
        {
            print("day");
            currentIntensity -= Time.deltaTime;
        }
        volume.gameObject.GetComponent<Volume>().weight = currentIntensity;
    }

}
