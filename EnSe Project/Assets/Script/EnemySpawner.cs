using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Enemy prefabs
    public GameObject heartPrefab; // Heart prefab
    public float spawnDistance = 10f;
    public float destroyDistance = 12f;
    public float spawnTriggerDistance = 10f;
    public int enemyBatchCount = 3;
    public float spawnInterval = 1f;

    private float lastSpawnX;
    private bool isSpawning = false;
    private HeartSpawner heartSpawner; // Reference to HeartSpawner

    void Start()
    {
        lastSpawnX = Camera.main.transform.position.x;

        // Find HeartSpawner anywhere in the scene
        heartSpawner = FindObjectOfType<HeartSpawner>();

        // Debugging to check if HeartSpawner is found
        if (heartSpawner == null)
        {
            Debug.LogError("HeartSpawner not found! Make sure it is in the scene.");
        }
    }

    void Update()
    {
        float cameraX = Camera.main.transform.position.x;

        if (cameraX - lastSpawnX >= spawnTriggerDistance && !isSpawning)
        {
            StartCoroutine(SpawnEnemyBatch());
            lastSpawnX = cameraX;
        }

        DestroyOffscreenEnemies();
    }

    IEnumerator SpawnEnemyBatch()
    {
        isSpawning = true;

        for (int i = 0; i < enemyBatchCount; i++)
        {
            Vector2 spawnPosition = SpawnEnemy();

            // 80% chance to spawn a heart
            if (Random.value < 0.8f && heartSpawner != null)
            {
                Debug.Log("Spawning Heart at: " + spawnPosition);
                heartSpawner.SpawnHeart(spawnPosition);
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    Vector2 SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return Vector2.zero;

        float spawnX = Camera.main.transform.position.x + spawnDistance;
        float spawnY = Random.Range(-2f, 2f);

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        GameObject enemyToSpawn = GetRandomEnemy();
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

        return spawnPosition;
    }

    GameObject GetRandomEnemy()
    {
        float randomValue = Random.value;

        if (randomValue <= 0.4f)
        { 
            return enemyPrefabs[0];
        }
        else if (randomValue <= 0.8f)
        {
            return enemyPrefabs[2];
        }
        else
        {
            return enemyPrefabs[1];
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
