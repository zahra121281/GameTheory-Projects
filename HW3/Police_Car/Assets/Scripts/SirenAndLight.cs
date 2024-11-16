using UnityEngine;

public class PoliceCarSiren : MonoBehaviour
{
    // private Light redLight;
    private Light blueLight;
    private AudioSource sirenSound;
    private float normalIntensity = 2000f;
    private float intensifiedIntensity = 5000f;
    private Light 
    void Start()
    {
        // Set initial light intensities
        // redLight.intensity = normalIntensity;
        blueLight.Intensity = normalIntensity;
        // sirenSound.Play(); // Start the siren sound
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Intensify the lights
            // redLight.intensity = intensifiedIntensity;
            blueLight.Intensity = intensifiedIntensity;

            // Adjust siren sound volume
            // sirenSound.volume = 1.0f; // Full volume
            sirenSound.Play(); // Start the siren sound
        }
        else
        {
            // Return lights to normal intensity
            // redLight.intensity = normalIntensity;
            blueLight.Intensity = normalIntensity;

            // Adjust siren sound volume
            // sirenSound.volume = 0.5f; // Lower volume
            sirenSound.Stop(); // Start the siren sound
        }
    }
}
