using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerID = 1; // Oyuncu ID'si (1 = Player1, 2 = Player2)
    public float moveSpeed = 5f; // Hareket hızı
    public float normalJumpForce = 5f; // Normal zıplama kuvveti
    public float highJumpForce = 8f; // Uzun basıldığında ekstra zıplama
    public float holdJumpThreshold = 0.3f; // Uzun basma süresi (300ms)
    public Transform cameraTransform; // Kamera referansı (sınırları belirlemek için)

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float jumpPressTime = 0f;

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

        // Kameranın sol ve sağ sınırlarını hesapla
        float cameraHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        float leftLimit = cameraTransform.position.x - cameraHalfWidth + 0.5f;
        float rightLimit = cameraTransform.position.x + cameraHalfWidth - 0.5f;

        // Oyuncunun konumunu sınırlar içinde tut
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
