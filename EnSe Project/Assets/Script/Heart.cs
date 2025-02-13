using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.collectPointSound);
            GameManager.instance.AddHeart(); // Update heart count
            Destroy(gameObject);
        }
    }
}
