using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float smoothSpeed = 0.125f; // Smooth camera movement speed
    public Vector3 offset = new Vector3(0, 0, -10); // Camera offset

    private float minX; // Minimum X position for the camera

    void Start()
    {
        // Set the initial camera position
        minX = transform.position.x;
    }

    void LateUpdate()
    {
        if (player1 != null && player2 != null)
        {
            // Get the average X position of both players
            float targetX = (player1.position.x + player2.position.x) / 2;

            // Update only if the new position is to the right (camera moves forward only)
            if (targetX > minX)
            {
                minX = targetX;
            }

            // Keep Y position fixed, update X smoothly
            Vector3 desiredPosition = new Vector3(minX, transform.position.y, offset.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
