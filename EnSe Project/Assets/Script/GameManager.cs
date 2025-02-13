using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameMenuCanvas; // Pause menu UI
    public GameObject winScreenCanvas; // Win screen UI
    public TMP_Text heartText; // Heart count UI
    public Button restartButton;
    public Button toggleMusicButton;
    public Button toggleAmbientButton; // New button for ambient sounds

    private int heartCount = 0;
    private int winCondition = 23; // Hearts required to win
    private bool isPaused = false; // Game pause state

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
        gameMenuCanvas.SetActive(false); // Hide menu at start
        winScreenCanvas.SetActive(false); // Hide win screen

        restartButton.onClick.AddListener(RestartGame);
        toggleMusicButton.onClick.AddListener(ToggleMusic);
        toggleAmbientButton.onClick.AddListener(ToggleAmbient); // Bind new button

        UpdateUI();
    }

    void Update()
    {
        // Open/Close menu with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
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
        ResumeGame(); // Resume before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleMusic()
    {
        SoundManager.instance.ToggleMusic();
    }

    public void ToggleAmbient()
    {
        SoundManager.instance.ToggleAmbientSounds();
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;
        gameMenuCanvas.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1; // Pause/Resume game
    }

    public void ResumeGame()
    {
        isPaused = false;
        gameMenuCanvas.SetActive(false);
        Time.timeScale = 1; // Resume game
    }

    public void PlayerDied()
    {
        Debug.Log("A player died. Restarting game...");
        RestartGame(); // Restart the game if a player dies
    }

}
