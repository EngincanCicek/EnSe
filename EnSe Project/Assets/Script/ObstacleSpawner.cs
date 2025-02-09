using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Engel prefab�
    public Transform cameraTransform; // Ana kamera
    public float spawnDistance = 12f; // Kameran�n sa��nda spawn mesafesi
    public float destroyDistance = 10f; // Kameran�n soluna ge�ince yok etme mesafesi
    public float minHeight = -1f, maxHeight = 3f; // Engel y�ksekli�i rastgele ayarlanacak

    private Vector3 lastObstaclePosition; // Son olu�turulan engelin pozisyonu

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
        // En son eklenen engel kamera s�n�r�na yakla��nca yeni engel �ret
        return lastObstaclePosition.x < cameraTransform.position.x + (cameraTransform.GetComponent<Camera>().orthographicSize * 2);
    }

    void SpawnObstacle()
    {
        float spawnX = lastObstaclePosition.x + Random.Range(5f, 8f); // Engel aral�klar�n� rastgele belirle
        float spawnY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        newObstacle.tag = "Obstacle"; // Y�netmek i�in etiket

        lastObstaclePosition = spawnPosition; // Son engelin pozisyonunu g�ncelle
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
