using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual void Die()
    {
        Destroy(gameObject); // Düþmaný yok et
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                // Eðer oyuncu az önce düþmaný öldürdüyse zarar görmeyecek
                if (player.killedEnemy) return;

                Debug.Log("Player yandan veya alttan çarptý, öldü!");
                player.Die(); // Oyuncuyu öldür
            }
        }
    }
}
