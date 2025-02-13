using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual void Die()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.enemyDeathSound);

        Destroy(gameObject); // Destroy enemy
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                Debug.Log("Player hit from the side or bottom, died!");
                player.Die(); // Kill player
            }
        }
    }
}
