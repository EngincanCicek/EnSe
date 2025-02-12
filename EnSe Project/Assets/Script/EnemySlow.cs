using UnityEngine;

public class EnemySlow : Enemy
{
    public float speed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.left; // Always starts moving left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1; // Normal falling speed
        rb.freezeRotation = true; // Prevents flipping/tumbling
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Apply only horizontal movement; gravity handles the falling speed
        rb.linearVelocity = new Vector2(moveDirection.x * speed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            moveDirection.x *= -1; // Reverse X direction
        }
        else if (collision.gameObject.CompareTag("Player")) // Kill player on contact
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
