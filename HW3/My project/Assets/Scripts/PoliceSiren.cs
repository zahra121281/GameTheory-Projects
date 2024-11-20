using UnityEngine;

public class PoliceSiren : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light redLight; // Assign your red light in the inspector
    [SerializeField] private Light blueLight; // Assign your blue light in the inspector
    [SerializeField] private Transform sirenParent; // Assign the parent object of lights for rotation
    [SerializeField] private float normalIntensity = 1f; // Normal light intensity
    [SerializeField] private float boostedIntensity = 8f; // Intensity when siren is active

    [Header("Sound")]
    [SerializeField] private AudioSource sirenSound; // Assign an AudioSource with a siren clip

    [Header("Material Emission")]
    [SerializeField] private Renderer sirenModelRenderer; // Assign the siren model
    [SerializeField] private Color emissionColor = Color.white; // Set the emission color

    private bool isSirenBoosted = false; // Toggle for boosted siren mode

    private void Start()
    {
        // Set initial states
        SetLightIntensity(normalIntensity);
        SetSirenMaterialEmission(true); // Enable emission at start
    }

    private void Update()
    {
        // Toggle siren boost with the Space key
        if (Input.GetKeyDown(KeyCode.Space) && isSirenBoosted == false)
        {
            isSirenBoosted = true;
            SetLightIntensity(boostedIntensity);
            StartSirenSound();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isSirenBoosted == true)
        {
            isSirenBoosted = false;
            SetLightIntensity(normalIntensity);
            StopSirenSound();
        }

        // Rotate siren lights
        RotateSiren();
    }

    private void RotateSiren()
    {
        if (sirenParent != null)
        {
            sirenParent.Rotate(Vector3.up * 100 * Time.deltaTime);
        }
    }

    private void SetLightIntensity(float intensity)
    {
        if (redLight != null)
        {
            redLight.intensity = intensity;
            Debug.Log("Red Light Intensity: " + redLight.intensity);
        }

        if (blueLight != null)
        {
            blueLight.intensity = intensity;
            Debug.Log("Blue Light Intensity: " + blueLight.intensity);
        }
    }


    private void StartSirenSound()
    {
        if (sirenSound != null && !sirenSound.isPlaying)
        {
            sirenSound.loop = true; // Ensure looping
            sirenSound.Play();
        }
    }

    private void StopSirenSound()
    {
        if (sirenSound != null && sirenSound.isPlaying)
        {
            sirenSound.Stop();
        }
    }

    private void SetSirenMaterialEmission(bool state)
    {
        if (sirenModelRenderer != null)
        {
            if (state)
            {
                sirenModelRenderer.material.EnableKeyword("_EMISSION");
                sirenModelRenderer.material.SetColor("_EmissionColor", emissionColor);
            }
            else
            {
                sirenModelRenderer.material.DisableKeyword("_EMISSION");
            }
        }
    }
}
