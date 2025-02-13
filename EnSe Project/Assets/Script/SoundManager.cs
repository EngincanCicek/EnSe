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
    private bool ambientEnabled = true; // Ambient sounds toggle

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
        ambientSource.volume = 0.25f; // Lower volume for ambient sounds

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

    public void ToggleAmbientSounds()
    {
        ambientEnabled = !ambientEnabled;

        if (!ambientEnabled && ambientSource.isPlaying)
        {
            ambientSource.Stop(); // Stop currently playing ambient sound immediately
            isPlayingAmbient = false;
        }

        Debug.Log("Ambient sounds " + (ambientEnabled ? "enabled" : "disabled"));
    }

    private IEnumerator PlayRandomAmbientSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Check every second

            if (ambientEnabled && !isPlayingAmbient && ambientSounds.Length > 0 && Time.timeScale > 0)
            {
                if (Random.value < 0.05f) // 5% chance per second
                {
                    isPlayingAmbient = true;
                    AudioClip randomClip = ambientSounds[Random.Range(0, ambientSounds.Length)];
                    ambientSource.PlayOneShot(randomClip);
                    Debug.Log("Playing ambient sound: " + randomClip.name);

                    // Wait until the sound finishes playing or ambient is disabled
                    float elapsedTime = 0f;
                    while (elapsedTime < randomClip.length && ambientEnabled)
                    {
                        yield return new WaitForSeconds(0.1f);
                        elapsedTime += 0.1f;
                    }

                    isPlayingAmbient = false;
                }
            }
        }
    }
}
