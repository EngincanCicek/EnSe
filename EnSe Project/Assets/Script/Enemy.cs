using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual void Die()
    {
        Destroy(gameObject); // D��man� yok et
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                // E�er oyuncu az �nce d��man� �ld�rd�yse zarar g�rmeyecek
                if (player.killedEnemy) return;

                Debug.Log("Player yandan veya alttan �arpt�, �ld�!");
                player.Die(); // Oyuncuyu �ld�r
            }
        }
    }
}
