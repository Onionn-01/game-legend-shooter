using UnityEngine;
using UnityEngine.Audio; // Bắt buộc có để điều khiển Mixer

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Mixer Groups")]
    public AudioMixerGroup musicGroup; // Kéo nhóm Music từ Mixer vào đây
    public AudioMixerGroup sfxGroup;   // Kéo nhóm SFX từ Mixer vào đây

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip shootSound;
    public AudioClip enemyDieSound;
    public AudioClip playerHitSound;

    private AudioSource musicSource;
    private AudioSource sfxSource;

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
        // Khởi tạo loa cho Nhạc
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = musicGroup; // Cắm dây vào cổng Music

        // Khởi tạo loa cho Hiệu ứng (Súng, Quái...)
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = sfxGroup;   // Cắm dây vào cổng SFX

        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}