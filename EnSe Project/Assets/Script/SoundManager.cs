using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; //Singleton basic

    private AudioSource audioSource;
    private AudioSource musicSource; 

    [Header("Sound Effects")]
    public AudioClip collectPointSound; 
    public AudioClip enemyFlySound; 
    public AudioClip enemyJumpSound; 
    public AudioClip playerJumpSound;
    public AudioClip playerDeathSound; 
    public AudioClip enemyDeathSound; 
    public AudioClip gameOverSound; 

    [Header("Background Music")]
    public AudioClip backgroundMusic; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = 0.5f; 
        musicSource.Play(); 
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
}
