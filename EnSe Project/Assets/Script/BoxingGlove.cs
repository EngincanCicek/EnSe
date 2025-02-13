using UnityEngine;

public class BoxingGlove : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Boxing Glove hit an enemy!");

            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null)
            {
                Debug.Log("Enemy is hit by boxing glove and will jump!");
                enemy.Die();
            }

            Destroy(gameObject); // Destroy the boxing glove
        }
    }
}
