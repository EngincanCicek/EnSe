using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Farklý düþman türlerini içeren dizi
    public float spawnDistance = 10f; // Kameranýn saðýnda spawn olma mesafesi
    public float destroyDistance = 12f; // Kameranýn solunda yok edilme mesafesi
    public float spawnTriggerDistance = 10f; // Kamera her 10 birim ilerlediðinde yeni batch baþlar
    public int enemyBatchCount = 3; // Her 10 birim hareket baþýna kaç düþman spawn edilecek
    public float spawnInterval = 1f; // Düþmanlar kaç saniye arayla spawn edilecek

    private float lastSpawnX; // En son ne zaman batch tetiklendi
    private bool isSpawning = false; // Þu anda batch spawnlanýyor mu?

    void Start()
    {
        lastSpawnX = Camera.main.transform.position.x;
    }

    void Update()
    {
        float cameraX = Camera.main.transform.position.x;

        // Kamera her 10 birim hareket ettiðinde ve þu anda spawn iþlemi yoksa, yeni batch baþlat
        if (cameraX - lastSpawnX >= spawnTriggerDistance && !isSpawning)
        {
            StartCoroutine(SpawnEnemyBatch());
            lastSpawnX = cameraX; // Yeni spawn iþlemi için X konumunu kaydet
        }

        DestroyOffscreenEnemies();
    }

    IEnumerator SpawnEnemyBatch()
    {
        isSpawning = true; // Spawn iþlemi baþladý

        for (int i = 0; i < enemyBatchCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // Her düþman belirlenen zaman aralýðýnda spawn olacak
        }

        isSpawning = false; // Batch tamamlandý, tekrar tetiklenebilir
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
