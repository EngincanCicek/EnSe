using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Platform prefab
    public Transform cameraTransform; // Main camera
    public float platformWidth = 15f; // Platform width
    public float minHeight = -2f, maxHeight = 2f; // Random height range
    public int platformCount = 5; // Number of platforms to spawn initially

    private Vector3 lastPlatformPosition; // Last spawned platform position
    private int spawnedPlatforms = 0; // Track the number of spawned platforms

    void Start()
    {
        // Spawn the initial set of platforms
        lastPlatformPosition = transform.position;
        for (int i = 0; i < platformCount; i++)
        {
            SpawnPlatform();
        }
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
        return spawnedPlatforms < platformCount || lastPlatformPosition.x < cameraTransform.position.x + (cameraTransform.GetComponent<Camera>().orthographicSize * 2);
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

        // Ensure the number of spawned platforms remains consistent
        spawnedPlatforms++;
    }

    void DestroyOldPlatforms()
    {
        float cameraLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;

        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            if (platform.transform.position.x + platformWidth < cameraLeftEdge)
            {
                Destroy(platform);
                spawnedPlatforms--; // Adjust the count when platforms are removed
            }
        }
    }
}
