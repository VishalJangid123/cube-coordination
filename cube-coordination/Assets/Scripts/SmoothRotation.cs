using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    public float rotationSpeed = 90.0f; // Rotation speed in degrees per second
    public float interval = 2.0f; // Rotation interval in seconds

    public float rotationTimer = 0.0f;
    private bool isRotating = false;

    private void Update()
    {
        if (!isRotating)
        {
            rotationTimer += Time.deltaTime;
            if (rotationTimer >= interval)
            {
                // Start the rotation when the interval is reached
                isRotating = true;
                rotationTimer = 0.0f;
            }
        }
        else
        {
            // Rotate the object smoothly
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationAmount);

            // Stop the rotation when the interval is reached
            if (rotationTimer >= interval)
            {
                isRotating = false;
                rotationTimer = 0.0f;
            }
        }
    }
}
