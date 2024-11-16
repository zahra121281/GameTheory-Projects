using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f; // Speed of movement

    void Update()
    {
        // Get input from arrow keys
        float horizontal = Input.GetAxis("Horizontal"); // Left/Right movement
        float vertical = Input.GetAxis("Vertical");     // Forward/Backward movement

        // Calculate new position
        Vector3 newPosition = transform.position;
        newPosition.x += horizontal * speed * Time.deltaTime;
        newPosition.z += vertical * speed * Time.deltaTime;

        // Apply the new position to the car
        transform.position = newPosition;
    }
}
