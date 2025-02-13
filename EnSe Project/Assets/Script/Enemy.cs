using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual void Die()
    {
        if (SoundManager.instance != null && SoundManager.instance.enemyDeathSound != null)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.enemyDeathSound);
        }

        Destroy(gameObject); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                Debug.Log("Player hit from the side or bottom, died!");
                player.Die(); 
            }
        }
    }
}
