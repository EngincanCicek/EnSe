using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Farkl� d��man t�rlerini i�eren dizi
    public float spawnDistance = 10f; // Kameran�n sa��nda spawn olma mesafesi
    public float destroyDistance = 12f; // Kameran�n solunda yok edilme mesafesi
    public float spawnTriggerDistance = 10f; // Kamera her 10 birim ilerledi�inde yeni batch ba�lar
    public int enemyBatchCount = 3; // Her 10 birim hareket ba��na ka� d��man spawn edilecek
    public float spawnInterval = 1f; // D��manlar ka� saniye arayla spawn edilecek

    private float lastSpawnX; // En son ne zaman batch tetiklendi
    private bool isSpawning = false; // �u anda batch spawnlan�yor mu?

    void Start()
    {
        lastSpawnX = Camera.main.transform.position.x;
    }

    void Update()
    {
        float cameraX = Camera.main.transform.position.x;

        // Kamera her 10 birim hareket etti�inde ve �u anda spawn i�lemi yoksa, yeni batch ba�lat
        if (cameraX - lastSpawnX >= spawnTriggerDistance && !isSpawning)
        {
            StartCoroutine(SpawnEnemyBatch());
            lastSpawnX = cameraX; // Yeni spawn i�lemi i�in X konumunu kaydet
        }

        DestroyOffscreenEnemies();
    }

    IEnumerator SpawnEnemyBatch()
    {
        isSpawning = true; // Spawn i�lemi ba�lad�

        for (int i = 0; i < enemyBatchCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // Her d��man belirlenen zaman aral���nda spawn olacak
        }

        isSpawning = false; // Batch tamamland�, tekrar tetiklenebilir
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
