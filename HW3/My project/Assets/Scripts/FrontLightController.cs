using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLightController : MonoBehaviour
{
    // Front light components
    [SerializeField] private Light frontLeftLight;
    [SerializeField] private Light frontRightLight;

    // Light state
    private bool areLightsOn = false;

    private void Update()
    {
        // Toggle lights when "G" key is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleLights();
        }
    }

    private void ToggleLights()
    {
        // Switch the light state
        areLightsOn = !areLightsOn;

        // Update light components
        if (frontLeftLight != null) frontLeftLight.enabled = areLightsOn;
        if (frontRightLight != null) frontRightLight.enabled = areLightsOn;
    }
}