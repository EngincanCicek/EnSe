using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Platform prefabý
    public Transform cameraTransform; // Ana kamera
    public float platformWidth = 5f; // Platform geniþliði
    public float minHeight = -2f, maxHeight = 2f; // Rastgele yükseklik sýnýrlarý

    private Vector3 lastPlatformPosition; // En son oluþturulan platformun pozisyonu

    void Start()
    {
        // Ýlk platformu yerleþtir
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
        // En son eklenen platform kamera sýnýrýna yaklaþýnca yeni platform üret
        return lastPlatformPosition.x < cameraTransform.position.x + (cameraTransform.GetComponent<Camera>().orthographicSize * 2);
    }

    void SpawnPlatform()
    {
        float spawnX = lastPlatformPosition.x + platformWidth;
        float spawnY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        newPlatform.tag = "Platform"; // Platformlarý yönetmek için etiket

        // Son oluþturulan platformun pozisyonunu güncelle
        lastPlatformPosition = spawnPosition;
    }

    void DestroyOldPlatforms()
    {
        // Kameranýn sol sýnýrýnýn dünya koordinatlarýný al
        float cameraLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;

        // "Platform" etiketi olan tüm nesneleri kontrol et
        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            // Platformun sol sýnýrýnýn kamera dýþýna çýkýp çýkmadýðýný kontrol et
            if (platform.transform.position.x + platformWidth < cameraLeftEdge)
            {
                Destroy(platform);
            }
        }
    }
}
