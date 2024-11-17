using UnityEngine;

public class BackLightController : MonoBehaviour
{
    // Brake light components
    [SerializeField] private Light rearLeftBrakeLight;
    [SerializeField] private Light rearRightBrakeLight;

    private void Update()
    {
        // Check if the S key is pressed
        bool isBraking = Input.GetKey(KeyCode.S);

        // Update the brake lights based on braking state
        if (rearLeftBrakeLight != null) rearLeftBrakeLight.enabled = isBraking;
        if (rearRightBrakeLight != null) rearRightBrakeLight.enabled = isBraking;
    }
}
