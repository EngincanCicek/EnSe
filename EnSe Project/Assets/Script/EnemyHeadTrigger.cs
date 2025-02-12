using UnityEngine;

public class EnemyHeadTrigger : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                enemy.Die();

                // Small jump effect
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5f);
                }
            }
        }
        else if (collision.CompareTag("BoxingGlove"))
        {
            Destroy(enemy.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
