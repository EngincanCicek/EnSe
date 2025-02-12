using UnityEngine;

public class BoxingGlove : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }

            Destroy(gameObject);
        }
    }
}
