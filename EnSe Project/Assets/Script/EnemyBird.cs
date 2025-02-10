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

        // Yerçekimini kapat, rotation'u dondur
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        // Başlangıç yüksekliğini kaydet
        initialY = transform.position.y;

        // Rastgele zaman ofseti ekleyerek düşmanların aynı dalgada olmamasını sağla
        timeOffset = Random.Range(0f, Mathf.PI * 2);
    }

    void FixedUpdate()
    {
        // Dikey sinüs dalgası hareketi
        float verticalMovement = Mathf.Sin(Time.time * 2f + timeOffset) * flyHeight;

        // Rigidbody'yi kullanarak yatay ve dikey hareketi aynı anda uygula
        rb.linearVelocity = new Vector2(-speed, verticalMovement);
    }
}
