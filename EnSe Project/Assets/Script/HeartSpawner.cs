using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab; // Heart prefab
    public float heartSpawnHeight = 3f; // Fixed Y position

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
}
