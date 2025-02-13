using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Singleton

    private AudioSource audioSource;
    private AudioSource musicSource;
    private AudioSource ambientSource; // Separate source for ambient sounds

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

    [Header("Ambient Sounds")]
    public AudioClip[] ambientSounds; // Array of ambient sounds

    private bool isPlayingAmbient = false; // Prevents overlapping ambient sounds

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
            return;
        }
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = 0.5f;
        musicSource.Play();

        ambientSource = gameObject.AddComponent<AudioSource>();
        ambientSource.loop = false;
        ambientSource.volume = 0.25f; // 0.25 Half volume compared to normal effects

        StartCoroutine(PlayRandomAmbientSound());
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

    private IEnumerator PlayRandomAmbientSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Check every second

            if (!isPlayingAmbient && ambientSounds.Length > 0 && Time.timeScale > 0) // Only play if game is running
            {
                if (Random.value < 0.5f) // 25% chance per second
                {
                    isPlayingAmbient = true;
                    AudioClip randomClip = ambientSounds[Random.Range(0, ambientSounds.Length)];
                    ambientSource.PlayOneShot(randomClip);
                    Debug.Log("Playing ambient sound: " + randomClip.name);

                    // Wait until the sound finishes playing
                    yield return new WaitForSeconds(randomClip.length);
                    isPlayingAmbient = false;
                }
            }
        }
    }
}
