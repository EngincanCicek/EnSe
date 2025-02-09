using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Farklý düþman türlerini içeren dizi
    public float spawnDistance = 10f; // Kameranýn saðýnda spawn olma mesafesi
    public float destroyDistance = 12f; // Kameranýn solunda yok edilme mesafesi
    public float spawnRate = 3f; // Kaç saniyede bir düþman spawn olacak

    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }

        DestroyOffscreenEnemies();
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;

        // Kamera pozisyonuna göre spawn noktasý belirle
        float spawnX = Camera.main.transform.position.x + spawnDistance;
        float spawnY = Random.Range(-2f, 2f); // Farklý yüksekliklerde spawn olmasý için rastgele Y

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        GameObject enemyToSpawn = GetRandomEnemy();
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    GameObject GetRandomEnemy()
    {
        // Düþman ihtimalleri (%50 yavaþ, %30 hýzlý, %20 zýplayan)
        float randomValue = Random.value;

        if (randomValue <= 0.5f) // %50 Yavaþ düþman
        {
            return enemyPrefabs[0];
        }
        else if (randomValue <= 0.8f) // %30 Hýzlý düþman
        {
            return enemyPrefabs[1];
        }
        else // %20 Zýplayan düþman
        {
            return enemyPrefabs[2];
        }
    }

    void DestroyOffscreenEnemies()
    {
        float cameraLeftEdge = Camera.main.transform.position.x - destroyDistance;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.transform.position.x < cameraLeftEdge)
            {
                Destroy(enemy);
            }
        }
    }
}
