using UnityEngine;

public class EnemyHeadTrigger : MonoBehaviour
{
    private Enemy enemy; // Bağlı olduğu düşmanı takip eder

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>(); // Enemy parent'ını al
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                // Oyuncu düşmanı öldürdü, "killedEnemy" flag'ini aç (100ms sonra kapanacak)
                player.SetKilledEnemy();

                // Düşmanı yok et
                enemy.Die();

                // Küçük bir yukarı zıplama efekti (Mario gibi)
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5f);
                }
            }
        }
    }
}
