using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab; // Heart prefab
    public float heartSpawnHeight = 3f; // Fixed Y position
    public float minYThreshold = -5f; // Minimum Y position before heart is destroyed
    public float leftBoundaryOffset = 2f; // How far left of the camera before heart is destroyed

    void Update()
    {
        DestroyOutOfBoundsHearts();
    }

    public void SpawnHeart(Vector2 spawnPosition)
    {
        if (heartPrefab == null)
        {
            Debug.LogError("Heart prefab is missing! Assign it in the Inspector.");
            return;
        }

        Vector2 heartPosition = new Vector2(spawnPosition.x, heartSpawnHeight);
        Instantiate(heartPrefab, heartPosition, Quaternion.identity);

        Debug.Log("Heart spawned at: " + heartPosition);
    }

    void DestroyOutOfBoundsHearts()
    {
        if (Camera.main == null) return;

        float cameraLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        foreach (GameObject heart in GameObject.FindGameObjectsWithTag("Heart"))
        {
            if (heart.transform.position.y < minYThreshold || heart.transform.position.x < cameraLeftEdge - leftBoundaryOffset)
            {
                Destroy(heart);
            }
        }
    }
}
