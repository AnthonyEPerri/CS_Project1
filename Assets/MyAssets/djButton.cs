﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class djButton : MonoBehaviour
{
	public GameObject animation_panel;
    public GameObject sensor_panel;

    Vector3 restingPosition = new Vector3(0.0f, -0.3f,  0.0f);
    Vector3 maxPressedPosition = new Vector3( 0.0f, -0.2f, 0.0f);

    [SerializeField]
    float buttonSpringStrength = 1.5f;

    float pressForce;
    float pressedProgress;
    bool collision;

    public bool pressed;

    [SerializeField]
    Material pressedMaterial;

    Material baseMaterial;

    float timeSinceLastPress;
    float pressTimeout = 0.1f;

    // Use this for initialization
    void Start () {
        baseMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {

        pressedProgress = pressForce / buttonSpringStrength;
        transform.localPosition = Vector3.Lerp(restingPosition, maxPressedPosition, pressedProgress);

        if(pressedProgress >= 0.8f)
        {
            pressed = true;
            GetComponent<Renderer>().material = pressedMaterial;
        }
        else
        {
            pressed = false;
            GetComponent<Renderer>().material = baseMaterial;
        }

        if(!collision && pressForce > 0)
        {
            pressForce -= Time.deltaTime * buttonSpringStrength * 10;
            if (pressForce < 0)
                pressForce = 0;
        }

        if (timeSinceLastPress < pressTimeout && collision)
        {
            timeSinceLastPress += Time.deltaTime;
        }
        else if (timeSinceLastPress >= pressTimeout && collision)
        {
            collision = false;
        }
    }

    void OnCollisionStay(Collision c)
    {
        pressForce = c.impulse.magnitude;
        collision = true;

        timeSinceLastPress = 0;
    }

    void OnCollisionExit()
    {
        collision = false;
    }
}
