using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float smoothSpeed = 0.125f; // Kameranýn yumuþak hareket etme hýzý
    public Vector3 offset = new Vector3(0, 0, -10); // Kamera mesafesi

    private float minX; // Kameranýn gidebileceði minimum X deðeri

    void Start()
    {
        // Kameranýn baþlangýç pozisyonunu belirle
        minX = transform.position.x;
    }

    void LateUpdate()
    {
        if (player1 != null && player2 != null)
        {
            // Ýki oyuncunun X pozisyonlarýnýn ortalamasýný al
            float targetX = (player1.position.x + player2.position.x) / 2;

            // Eðer yeni pozisyon mevcut kameranýn solunda kalýyorsa, onu güncelleme (Sadece saða gider)
            if (targetX > minX)
            {
                minX = targetX;
            }

            // Kameranýn Y'si deðiþmez, X'ini yavaþça güncelle
            Vector3 desiredPosition = new Vector3(minX, transform.position.y, offset.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
