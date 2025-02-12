using UnityEngine;

public class NearestPlayerFinder
{
    private Transform targetPlayer;

    public void FindNearestPlayer(Vector3 enemyPosition)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = Mathf.Infinity;
        Transform closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector2.Distance(enemyPosition, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlayer = player.transform;
            }
        }

        targetPlayer = closestPlayer;
    }

    public Vector3 GetTargetPosition(Vector3 enemyPosition)
    {
        FindNearestPlayer(enemyPosition);
        return targetPlayer != null ? targetPlayer.position : enemyPosition;
    }

    public bool IsObstacleBetweenPlayer(Vector3 enemyPosition, LayerMask obstacleLayer)
    {
        if (targetPlayer == null) return false;

        Vector2 direction = (targetPlayer.position - enemyPosition).normalized;
        float distance = Vector2.Distance(enemyPosition, targetPlayer.position);

        RaycastHit2D hit = Physics2D.Raycast(enemyPosition, direction, distance, obstacleLayer);
        return hit.collider != null;
    }
}
