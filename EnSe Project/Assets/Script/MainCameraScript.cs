using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float smoothSpeed = 0.125f; // Kameran�n yumu�ak hareket etme h�z�
    public Vector3 offset = new Vector3(0, 0, -10); // Kamera mesafesi

    private float minX; // Kameran�n gidebilece�i minimum X de�eri

    void Start()
    {
        // Kameran�n ba�lang�� pozisyonunu belirle
        minX = transform.position.x;
    }

    void LateUpdate()
    {
        if (player1 != null && player2 != null)
        {
            // �ki oyuncunun X pozisyonlar�n�n ortalamas�n� al
            float targetX = (player1.position.x + player2.position.x) / 2;

            // E�er yeni pozisyon mevcut kameran�n solunda kal�yorsa, onu g�ncelleme (Sadece sa�a gider)
            if (targetX > minX)
            {
                minX = targetX;
            }

            // Kameran�n Y'si de�i�mez, X'ini yava��a g�ncelle
            Vector3 desiredPosition = new Vector3(minX, transform.position.y, offset.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
