using UnityEngine;

public class EnemyBird : Enemy
{
    public float flyHeight = 1f;
    public float speed = 1f;

    void Update()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, Mathf.Sin(Time.time) * flyHeight, transform.position.z);
    }
}
