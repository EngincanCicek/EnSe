using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Engel prefabý
    public Transform cameraTransform; // Ana kamera
    public float spawnDistance = 12f; // Kameranýn saðýnda spawn mesafesi
    public float destroyDistance = 10f; // Kameranýn soluna geçince yok etme mesafesi
    public float minHeight = -1f, maxHeight = 3f; // Engel yüksekliði rastgele ayarlanacak

    private Vector3 lastObstaclePosition; // Son oluþturulan engelin pozisyonu

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
        // En son eklenen engel kamera sýnýrýna yaklaþýnca yeni engel üret
        return lastObstaclePosition.x < cameraTransform.position.x + (cameraTransform.GetComponent<Camera>().orthographicSize * 2);
    }

    void SpawnObstacle()
    {
        float spawnX = lastObstaclePosition.x + Random.Range(5f, 8f); // Engel aralýklarýný rastgele belirle
        float spawnY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        newObstacle.tag = "Obstacle"; // Yönetmek için etiket

        lastObstaclePosition = spawnPosition; // Son engelin pozisyonunu güncelle
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
