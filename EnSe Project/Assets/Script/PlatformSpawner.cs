using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Platform prefab�
    public Transform cameraTransform; // Ana kamera
    public float platformWidth = 5f; // Platform geni�li�i
    public float minHeight = -2f, maxHeight = 2f; // Rastgele y�kseklik s�n�rlar�

    private Vector3 lastPlatformPosition; // En son olu�turulan platformun pozisyonu

    void Start()
    {
        // �lk platformu yerle�tir
        lastPlatformPosition = transform.position;
        SpawnPlatform();
    }

    void Update()
    {
        if (ShouldSpawnNewPlatform())
        {
            SpawnPlatform();
        }

        DestroyOldPlatforms();
    }

    bool ShouldSpawnNewPlatform()
    {
        // En son eklenen platform kamera s�n�r�na yakla��nca yeni platform �ret
        return lastPlatformPosition.x < cameraTransform.position.x + (cameraTransform.GetComponent<Camera>().orthographicSize * 2);
    }

    void SpawnPlatform()
    {
        float spawnX = lastPlatformPosition.x + platformWidth;
        float spawnY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        newPlatform.tag = "Platform"; // Platformlar� y�netmek i�in etiket

        // Son olu�turulan platformun pozisyonunu g�ncelle
        lastPlatformPosition = spawnPosition;
    }

    void DestroyOldPlatforms()
    {
        // Kameran�n sol s�n�r�n�n d�nya koordinatlar�n� al
        float cameraLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;

        // "Platform" etiketi olan t�m nesneleri kontrol et
        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            // Platformun sol s�n�r�n�n kamera d���na ��k�p ��kmad���n� kontrol et
            if (platform.transform.position.x + platformWidth < cameraLeftEdge)
            {
                Destroy(platform);
            }
        }
    }
}
