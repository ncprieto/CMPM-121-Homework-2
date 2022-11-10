using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DayNightCyclescript : MonoBehaviour
{   
    //based on https://youtube.com/watch?v=33RL196x4LI

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;
    // skip other lighting from tutorial
    [Header("Head Lamp")]
    public Light headLamp;

    [Header("Flood Light")]
    public Light floodLight;
    // Start is called before the first frame update
    // Update is called once per frame
    private bool passedNoon;
    void Start(){
        passedNoon = false;
    }
    void Update()
    {
        sun.transform.Rotate((Time.deltaTime - 0.05f), 0.0f, 0.0f);
        moon.transform.Rotate((Time.deltaTime - 0.05f), 0.0f, 0.0f);
        Vector3 sunRotation = sun.transform.rotation.eulerAngles;
        Debug.Log(sunRotation[0]);
        if(sunRotation[0] > 300 && sunRotation[0] < 340){
            passedNoon = false;
            sun.intensity = 0;
        }
        else{
            if(sunRotation[0] >= 50 && sunRotation[0] < 60){
                passedNoon = true;
            }
            if(sunRotation[0] > 0 ){
                if(passedNoon){
                    sun.intensity -= 0.001f;
                }
                else if(!passedNoon){
                    sun.intensity += 0.001f;
                }
            }
        }
        // if(sun.transform.rotation.x < 0){
        //     if(sun.transform.rotation.x <= -0.5){
        //         passedNoon = true;
        //     }
        //     if(passedNoon){
        //         sun.intensity -= 0.001f;
        //     }
        //     else{
        //         sun.intensity += 0.001f;
        //     }
        // }
        // else{
        //     passedNoon = false;
        //     sun.intensity = 0;
        // }
    }
}
