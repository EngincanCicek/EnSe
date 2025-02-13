using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameMenuCanvas; // Main menu UI
    public GameObject winScreenCanvas; // Win screen UI
    public TMP_Text heartText; // Heart count UI
    public Button restartButton;
    public Button toggleMusicButton;

    private int heartCount = 0;
    private int winCondition = 23; // Hearts required to win

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameMenuCanvas.SetActive(true); // Show menu at start
        winScreenCanvas.SetActive(false); // Hide win screen

        restartButton.onClick.AddListener(RestartGame);
        toggleMusicButton.onClick.AddListener(ToggleMusic);

        UpdateUI();
    }

    public void AddHeart()
    {
        heartCount++;
        UpdateUI();

        if (heartCount >= winCondition)
        {
            WinGame();
        }
    }

    void UpdateUI()
    {
        if (heartText != null)
        {
            heartText.text = "Hearts: " + heartCount;
        }
    }

    void WinGame()
    {
        winScreenCanvas.SetActive(true);
        gameMenuCanvas.SetActive(false);
        Debug.Log("You Win!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleMusic()
    {
        SoundManager.instance.ToggleMusic();
    }
}
