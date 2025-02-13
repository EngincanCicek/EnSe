using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.collectPointSound);

            HeartManager.instance.AddHeart();
            Destroy(gameObject);
        }
    }
}
