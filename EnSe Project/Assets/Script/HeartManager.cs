using UnityEngine;
using TMPro;

public class HeartManager : MonoBehaviour
{
    public static HeartManager instance; // Singleton
    public TMP_Text heartText; // UI text
    private int heartCount = 0; // Collected hearts

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        UpdateUI(); 
    }

    public void AddHeart()
    {
        heartCount++; 
        UpdateUI(); 
    }

    private void UpdateUI()
    {
        if (heartText != null)
            heartText.text = "Hearts: " + heartCount; // Write to ui
    }
}
