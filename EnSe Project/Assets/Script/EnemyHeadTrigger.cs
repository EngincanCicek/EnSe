using UnityEngine;

public class EnemyHeadTrigger : MonoBehaviour
{
    private Enemy enemy; // Tracks the attached enemy

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>(); // Get the parent enemy
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                // Player killed the enemy, activate "killedEnemy" flag (resets after 100ms)
                player.SetKilledEnemy();

                // Destroy the enemy
                enemy.Die();

                // Apply a small jump effect (like in mario)
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5f);
                }
            }
        }
    }
}
