using UnityEngine;

public class EnemyBird : Enemy
{
    public float flyHeight = 1f;
    public float speed = 1f;
    private Rigidbody2D rb;
    private float initialY;
    private float timeOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Disable gravity and freeze rotation
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        // Save the initial Y position
        initialY = transform.position.y;

        // Add a random time offset to prevent all enemies from moving in sync
        timeOffset = Random.Range(0f, Mathf.PI * 2);
    }

    void FixedUpdate()
    {
        // Apply vertical movement using a sine wave
        float verticalMovement = Mathf.Sin(Time.time * 2f + timeOffset) * flyHeight;

        // Use Rigidbody to apply both horizontal and vertical movement
        rb.linearVelocity = new Vector2(-speed, verticalMovement);
    }
}
