using UnityEngine;

public class EnemyFast : Enemy
{
    public float speed = 2f;
    public float jumpForce = 5f;
    private float lastJumpTime = 0f;
    private float jumpCooldown = 1f;

    private Rigidbody2D rb;
    private Collider2D col;
    private NearestPlayerFinder playerFinder = new NearestPlayerFinder();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        col = GetComponent<Collider2D>();

        // Reduce friction to prevent sticking but keep collisions working
        if (col != null)
        {
            PhysicsMaterial2D noFriction = new PhysicsMaterial2D();
            noFriction.friction = 0f;
            noFriction.bounciness = 0.1f; // Small bounce to keep collision detection active
            col.sharedMaterial = noFriction;
        }
    }

    void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector3 targetPosition = playerFinder.GetTargetPosition(transform.position);
        float direction = Mathf.Sign(targetPosition.x - transform.position.x);

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
        {
            if (CanJump())
            {
                Jump();
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Die(); // Kill player on touch
            }
        }
    }

    bool CanJump()
    {
        return Time.time - lastJumpTime >= jumpCooldown;
    }

    void Jump()
    {
        lastJumpTime = Time.time;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}
