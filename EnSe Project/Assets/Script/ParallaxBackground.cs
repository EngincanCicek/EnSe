using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraTransform; 
    public float parallaxSpeed = 0.5f; 
    private float backgroundWidth; 
    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;

        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraPosition.x;

        transform.position += Vector3.right * deltaX * parallaxSpeed;

        if (cameraTransform.position.x - transform.position.x >= backgroundWidth)
        {
            transform.position += new Vector3(backgroundWidth * 2, 0, 0);
        }

        lastCameraPosition = cameraTransform.position;
    }
}
