using UnityEngine;

public class EnemyFast : Enemy
{
    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
