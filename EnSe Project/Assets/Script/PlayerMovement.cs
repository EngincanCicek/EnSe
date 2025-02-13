using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerID = 1;
    public float moveSpeed = 5f;
    public float normalJumpForce = 5f;
    public float highJumpForce = 8f;
    public float holdJumpThreshold = 0.3f;
    public Transform cameraTransform;
    public GameObject boxingGlovePrefab;
    public Transform gloveSpawnPoint;
    public float throwForce = 15f;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float jumpPressTime = 0f;
    private bool canThrow = true;
    private GameObject currentGlove;
    private bool isInvincible = false; // Prevents immediate death
    private Collider2D foot; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foot = GetComponentInChildren<Collider2D>();
        if (playerID == 2)
        {
            InvokeRepeating("SpawnBoxingGlove", 3f, 3f);
        }
    }

    void Update()
    {
        Move();
        HandleJump();
        ClampPosition();

        if (playerID == 2 && Input.GetMouseButtonDown(0) && currentGlove != null)
        {
            ThrowBoxingGlove();
        }

        if (currentGlove != null)
        {
            currentGlove.transform.position = gloveSpawnPoint.position;
        }
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

    void SpawnBoxingGlove()
    {
        if (!canThrow || boxingGlovePrefab == null || gloveSpawnPoint == null || currentGlove != null) return;

        currentGlove = Instantiate(boxingGlovePrefab, gloveSpawnPoint.position, Quaternion.identity);
        currentGlove.transform.SetParent(transform);
    }

    void ThrowBoxingGlove()
    {
        if (currentGlove == null) return;

        currentGlove.transform.SetParent(null);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector2 throwDirection = (mousePosition - currentGlove.transform.position).normalized;

        Rigidbody2D gloveRb = currentGlove.GetComponent<Rigidbody2D>();
        if (gloveRb != null)
        {
            gloveRb.gravityScale = 0;
            gloveRb.linearVelocity = throwDirection * throwForce;
        }

        Destroy(currentGlove, 0.25f);
        currentGlove = null;

        canThrow = false;
        Invoke("ResetThrow", 3f);
    }

    void ResetThrow()
    {
        canThrow = true;
    }

    void HandleJump()
    {
        Debug.Log("HandleJump");
        KeyCode jumpKey = (playerID == 1) ? KeyCode.W : KeyCode.UpArrow;

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            jumpPressTime = Time.time;
        }

        if (Input.GetKeyUp(jumpKey) && isGrounded)
        {
            float holdTime = Mathf.Clamp(Time.time - jumpPressTime,0.1f,1f);
            float jumpForce = (holdTime >= holdJumpThreshold) ? highJumpForce : normalJumpForce;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Debug.Log("Input.GetKeyUp");

        }
    }

    void ClampPosition()
    {
        if (cameraTransform == null) return;

        float cameraHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        float leftLimit = cameraTransform.position.x - cameraHalfWidth + 0.5f;
        float rightLimit = cameraTransform.position.x + cameraHalfWidth - 0.5f;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftLimit, rightLimit);
        transform.position = clampedPosition;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;
            Debug.Log("OnTriggerEnter2D");

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;
            Debug.Log("OnTriggerEnter2D");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = false;
            Debug.Log("OnTriggerExit2D");
        }
    }





    public void Die()
    {
        if (isInvincible) return; // Prevent death if recently killed an enemy

        Debug.Log("Player died!");
        gameObject.SetActive(false);
    }

    public void SetInvincible()
    {
        isInvincible = true;
        Invoke("ResetInvincibility", 0.1f); // 100ms invincibility
    }

    private void ResetInvincibility()
    {
        isInvincible = false;
    }
}
