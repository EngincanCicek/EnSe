using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerID = 1; // Player ID (1 = Player1, 2 = Player2)
    public float moveSpeed = 5f; // Movement speed
    public float normalJumpForce = 5f; // Regular jump force
    public float highJumpForce = 8f; // Extra jump force when holding
    public float holdJumpThreshold = 0.3f; // Hold duration for high jump (300ms)
    public Transform cameraTransform; // Camera reference for boundaries

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float jumpPressTime = 0f;
    public bool killedEnemy = false; // Becomes true when the player kills an enemy

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        HandleJump();
        ClampPosition();
    }

    void Move()
    {
        float moveInput = 0;

        if (playerID == 1)
        {
            if (Input.GetKey(KeyCode.A)) moveInput = -1;
            if (Input.GetKey(KeyCode.D)) moveInput = 1;
        }
        else if (playerID == 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) moveInput = -1;
            if (Input.GetKey(KeyCode.RightArrow)) moveInput = 1;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    public void Die()
    {
        if (killedEnemy)
        {
            return;
        }

        Debug.Log("Player died!");
        gameObject.SetActive(false);
    }

    public void SetKilledEnemy()
    {
        killedEnemy = true;
        Invoke("ResetKilledEnemy", 0.1f);
    }

    private void ResetKilledEnemy()
    {
        killedEnemy = false;
    }

    void HandleJump()
    {
        KeyCode jumpKey = (playerID == 1) ? KeyCode.W : KeyCode.UpArrow;

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            jumpPressTime = Time.time;
        }

        if (Input.GetKeyUp(jumpKey) && isGrounded)
        {
            float holdTime = Time.time - jumpPressTime;
            float jumpForce = (holdTime >= holdJumpThreshold) ? highJumpForce : normalJumpForce;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void ClampPosition()
    {
        if (cameraTransform == null) return;

        // Calculate left and right camera limits
        float cameraHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        float leftLimit = cameraTransform.position.x - cameraHalfWidth + 0.5f;
        float rightLimit = cameraTransform.position.x + cameraHalfWidth - 0.5f;

        // Keep player within screen boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftLimit, rightLimit);
        transform.position = clampedPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = false;
        }
    }
}
