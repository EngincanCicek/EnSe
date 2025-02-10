using UnityEngine;

public class EnemySlow : Enemy
{
    public float speed = 1f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
