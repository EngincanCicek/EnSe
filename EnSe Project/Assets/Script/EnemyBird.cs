using UnityEngine;

public class EnemyBird : Enemy
{
    public float flyHeight = 2f;
    public float speed = 20f;
    private Rigidbody2D rb;
    private float initialY;
    private float timeOffset;
    private NearestPlayerFinder playerFinder = new NearestPlayerFinder();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        initialY = transform.position.y;
        timeOffset = Random.Range(0f, Mathf.PI * 2);
    }

    void FixedUpdate()
    {
        MoveTowardsNearestPlayer();
    }

    void MoveTowardsNearestPlayer()
    {
        Vector3 targetPosition = playerFinder.GetTargetPosition(transform.position);
        float targetX = targetPosition.x;

        // Move towards the player's X position
        float newX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.fixedDeltaTime);

        // Apply vertical movement using a sine wave
        float newY = initialY + Mathf.Sin(Time.time * 2f + timeOffset) * flyHeight;

        // Apply movement
        rb.linearVelocity = new Vector2(newX - transform.position.x, newY - transform.position.y);
    }
}
