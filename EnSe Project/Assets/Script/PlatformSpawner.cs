using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Platform prefab
    public Transform cameraTransform; // Main camera
    public float platformWidth = 5f; // Platform width
    public float minHeight = -2f, maxHeight = 2f; // Random height range

    private Vector3 lastPlatformPosition; // Last spawned platform position

    void Start()
    {
        // Place the first platform
        lastPlatformPosition = transform.position;
        SpawnPlatform();
    }

    void Update()
    {
        if (ShouldSpawnNewPlatform())
        {
            SpawnPlatform();
        }

        DestroyOldPlatforms();
    }

    bool ShouldSpawnNewPlatform()
    {
        // Spawn a new platform when the last one is near the camera's edge
        return lastPlatformPosition.x < cameraTransform.position.x + (cameraTransform.GetComponent<Camera>().orthographicSize * 2);
    }

    void SpawnPlatform()
    {
        float spawnX = lastPlatformPosition.x + platformWidth;
        float spawnY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        newPlatform.tag = "Platform"; // Set tag for management

        // Update last platform position
        lastPlatformPosition = spawnPosition;
    }

    void DestroyOldPlatforms()
    {
        // Get the world position of the left camera edge
        float cameraLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;

        // Check all objects with the "Platform" tag
        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            // Destroy platforms that are off the left screen
            if (platform.transform.position.x + platformWidth < cameraLeftEdge)
            {
                Destroy(platform);
            }
        }
    }
}
