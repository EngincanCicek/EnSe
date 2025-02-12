using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Obstacle prefab
    public Transform cameraTransform; // Main camera
    public float spawnDistance = 20f; // Distance ahead of the camera to spawn obstacles
    public float destroyDistance = 10f; // Distance behind the camera to destroy obstacles
    public float minHeight = -1f, maxHeight = 3f; // Random obstacle height range
    public float minSpacing = 8f, maxSpacing = 12f; // Random spacing between obstacles
    public int initialObstacleCount = 5; // Number of obstacles to start with

    private Vector3 lastObstaclePosition; // Last spawned obstacle position

    void Start()
    {
        // Set initial position far ahead
        lastObstaclePosition = new Vector3(cameraTransform.position.x + spawnDistance, Random.Range(minHeight, maxHeight), 1);

        // Spawn multiple obstacles at start
        for (int i = 0; i < initialObstacleCount; i++)
        {
            SpawnObstacle();
        }
    }

    void Update()
    {
        if (ShouldSpawnNewObstacle())
        {
            SpawnObstacle();
        }

        DestroyOldObstacles();
    }

    bool ShouldSpawnNewObstacle()
    {
        return lastObstaclePosition.x < cameraTransform.position.x + spawnDistance;
    }

    void SpawnObstacle()
    {
        float spawnX = lastObstaclePosition.x + Random.Range(minSpacing, maxSpacing);
        float spawnY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 1);

        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        newObstacle.tag = "Obstacle"; // Set tag for management

        lastObstaclePosition = spawnPosition; // Update last obstacle position
    }

    void DestroyOldObstacles()
    {
        float cameraLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;

        foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            if (obstacle.transform.position.x < cameraLeftEdge - destroyDistance)
            {
                Destroy(obstacle);
            }
        }
    }
}
