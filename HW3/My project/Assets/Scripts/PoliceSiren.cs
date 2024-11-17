using UnityEngine;

public class PoliceSiren : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light redLight; // Assign your red light in the inspector
    [SerializeField] private Light blueLight; // Assign your blue light in the inspector
    [SerializeField] private Transform sirenParent; // Assign the parent object of lights for rotation
    [SerializeField] private float sirenSpeed = 1.0f; // Flash speed (seconds)

    [Header("Sound")]
    [SerializeField] private AudioSource sirenSound; // Assign an AudioSource with a siren clip

    [Header("Material Emission")]
    [SerializeField] private Renderer sirenModelRenderer; // Assign the siren model
    [SerializeField] private Color emissionColor = Color.white; // Set the emission color

    private bool isSirenActive = true; // Toggle for siren on/off
    private float timer;

    private void Start()
    {
        // Ensure initial states
        SetSirenLightsActive(true);
        StartSirenSound();
        SetSirenMaterialEmission(true);
    }

    private void Update()
    {
        // Toggle siren with the Space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSirenActive = !isSirenActive;
            SetSirenLightsActive(isSirenActive);
            if (isSirenActive) StartSirenSound(); else StopSirenSound();
            SetSirenMaterialEmission(isSirenActive);
        }

        if (isSirenActive)
        {
            SirenFlashEffect();
            RotateSiren();
        }
    }

    private void SirenFlashEffect()
    {
        timer += Time.deltaTime;

        if (timer >= sirenSpeed)
        {
            // Alternate light activity
            redLight.enabled = !redLight.enabled;
            blueLight.enabled = !blueLight.enabled;
            timer = 0f;
        }
    }

    private void RotateSiren()
    {
        if (sirenParent != null)
        {
            sirenParent.Rotate(Vector3.up * 100 * Time.deltaTime);
        }
    }

    private void SetSirenLightsActive(bool state)
    {
        if (redLight != null) redLight.enabled = state;
        if (blueLight != null) blueLight.enabled = state;
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
            // Enable or disable emission
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
