using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Obstacle prefab
    public Transform cameraTransform; // Main camera
    public float spawnDistance = 12f; // Distance ahead of the camera to spawn obstacles
    public float destroyDistance = 10f; // Distance behind the camera to destroy obstacles
    public float minHeight = -1f, maxHeight = 3f; // Random obstacle height range

    private Vector3 lastObstaclePosition; // Last spawned obstacle position

    void Start()
    {
        lastObstaclePosition = transform.position;
        SpawnObstacle();
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
        // Spawn a new obstacle when the last one gets close to the camera's edge
        return lastObstaclePosition.x < cameraTransform.position.x + (cameraTransform.GetComponent<Camera>().orthographicSize * 2);
    }

    void SpawnObstacle()
    {
        float spawnX = lastObstaclePosition.x + Random.Range(5f, 8f); // Randomize obstacle spacing
        float spawnY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

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
