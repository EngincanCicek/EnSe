using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Farkl� d��man t�rlerini i�eren dizi
    public float spawnDistance = 10f; // Kameran�n sa��nda spawn olma mesafesi
    public float destroyDistance = 12f; // Kameran�n solunda yok edilme mesafesi
    public float spawnRate = 3f; // Ka� saniyede bir d��man spawn olacak

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

        // Kamera pozisyonuna g�re spawn noktas� belirle
        float spawnX = Camera.main.transform.position.x + spawnDistance;
        float spawnY = Random.Range(-2f, 2f); // Farkl� y�ksekliklerde spawn olmas� i�in rastgele Y

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        GameObject enemyToSpawn = GetRandomEnemy();
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    GameObject GetRandomEnemy()
    {
        // D��man ihtimalleri (%50 yava�, %30 h�zl�, %20 z�playan)
        float randomValue = Random.value;

        if (randomValue <= 0.5f) // %50 Yava� d��man
        {
            return enemyPrefabs[0];
        }
        else if (randomValue <= 0.8f) // %30 H�zl� d��man
        {
            return enemyPrefabs[1];
        }
        else // %20 Z�playan d��man
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
