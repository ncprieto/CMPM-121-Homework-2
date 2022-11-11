using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DayNightCyclescript : MonoBehaviour
{   
    //based on https://youtube.com/watch?v=33RL196x4LI
    [Header("Sun")]
    public Light sun;

    [Header("Head Lamp")]
    public Light headLamp;

    [Header("Flood Light")]
    public Light floodLight1;
    public Light floodLight2;

    [Header("Dust Particles")]
    public GameObject dust;

    private bool passedNoon;
    private bool passedMidnight;
    private ParticleSystem dustParts;
    private bool played;

    void Start(){
        passedNoon = true;
        passedMidnight = false;
        dustParts = dust.gameObject.GetComponent<ParticleSystem>();
        played = false;
    }

    void Update()
    {
        sun.transform.Rotate((Time.deltaTime + 0.05f), 0.0f, 0.0f);
        Vector3 sunRotation = sun.transform.rotation.eulerAngles;
        // sun intensity logic
        if(sunRotation[0] >= 52 && sunRotation[0] < 55){
            passedNoon = true;
            passedMidnight = false;
        }
        if((sunRotation[0] >= 310 && sunRotation[0] <= 360) || (sunRotation[0] > 0 && sunRotation[0] <= 55)){
            sun.intensity += !passedNoon ? 0.0005f : -0.0005f;
        }
        else if(sunRotation[0] < 310 && sunRotation[0] > 300){
            passedNoon = false;
            passedMidnight = true;
            sun.intensity = 0;
        }
        // flood light logic for controlling slowing turning on lights
        // based on sun rotation so lights gradually become brighter as the sun comes up and goes down
        if(sunRotation[0] >= 10 && sunRotation[0] < 55){
            floodLight1.intensity = 0;
            floodLight2.intensity = 0;
            headLamp.intensity = 0;
        }
        if((sunRotation[0] < 10 && sunRotation[0] > 0) || (sunRotation[0] <= 360 && sunRotation[0] >= 320)){
            if(floodLight1.intensity < 5.5 && floodLight2.intensity < 5.5){
                if(!passedMidnight){
                    floodLight1.intensity += 0.005f;
                    floodLight2.intensity += 0.005f;
                    headLamp.intensity += 0.001f;
                }
            }
            if(passedMidnight){
                floodLight1.intensity -= 0.005f;
                floodLight2.intensity -= 0.005f;
                headLamp.intensity -= 0.005f;
            }
        }
        // play particle system once scene turns to night
        if(sunRotation[0] > 300 && !played){
            dustParts.Play();
            played = true;
        }
        // stop particle system once the scene turns to day
        else if(sunRotation[0] < 300){
            dustParts.Stop();
            played = false;
        }
    }
}
