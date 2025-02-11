using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy types
    public float spawnDistance = 10f; // Distance ahead of the camera to spawn enemies
    public float destroyDistance = 12f; // Distance behind the camera to destroy enemies
    public float spawnTriggerDistance = 10f; // Triggers a new batch every 10 units moved
    public int enemyBatchCount = 3; // Number of enemies per batch
    public float spawnInterval = 1f; // Time between enemy spawns in a batch

    private float lastSpawnX; // Last camera position when a batch was triggered
    private bool isSpawning = false; // Is a batch currently spawning?

    void Start()
    {
        lastSpawnX = Camera.main.transform.position.x;
    }

    void Update()
    {
        float cameraX = Camera.main.transform.position.x;

        // Start a new batch if the camera moved 10 units and no batch is active
        if (cameraX - lastSpawnX >= spawnTriggerDistance && !isSpawning)
        {
            StartCoroutine(SpawnEnemyBatch());
            lastSpawnX = cameraX;
        }

        DestroyOffscreenEnemies();
    }

    IEnumerator SpawnEnemyBatch()
    {
        isSpawning = true; // Start spawning

        for (int i = 0; i < enemyBatchCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next one
        }

        isSpawning = false; // Batch completed, ready for next trigger
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;

        // Define spawn position based on camera position
        float spawnX = Camera.main.transform.position.x + spawnDistance;
        float spawnY = Random.Range(-2f, 2f); // Random Y position for variation

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        GameObject enemyToSpawn = GetRandomEnemy();
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    GameObject GetRandomEnemy()
    {
        // Enemy spawn chances: 50% slow, 30% fast, 20% jumping
        float randomValue = Random.value;

        if (randomValue <= 0.5f) // 50% Slow enemy
        {
            return enemyPrefabs[0];
        }
        else if (randomValue <= 0.8f) // 30% Fast enemy
        {
            return enemyPrefabs[1];
        }
        else // 20% Jumping enemy
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
