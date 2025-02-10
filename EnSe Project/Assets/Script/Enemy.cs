using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual void Die()
    {
        Destroy(gameObject); // Destroy enemy
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                // kill cooldown for player
                if (player.killedEnemy) return;

                Debug.Log("Player yandan veya alttan çarptý, öldü!");
                player.Die(); // Kill player
            }
        }
    }
}
