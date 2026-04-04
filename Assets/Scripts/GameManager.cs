using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Panels")]
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    [Header("Audio Settings")]
    public AudioMixer mainMixer;
    public Slider musicSlider; // MỚI: Kéo Slider Music trong PausePanel vào đây
    public Slider sfxSlider;   // MỚI: Kéo Slider SFX trong PausePanel vào đây

    private bool isPaused = false;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    // --- HÀM START MỚI THÊM VÀO ---
    void Start()
    {
        // 1. Tải giá trị đã lưu từ PlayerPrefs (mặc định là 0.75 nếu chưa có)
        float savedMusic = PlayerPrefs.GetFloat("MusicVolSave", 0.75f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolSave", 0.75f);

        // 2. Cập nhật vị trí thanh trượt (nút kéo) trên UI
        if (musicSlider != null) musicSlider.value = savedMusic;
        if (sfxSlider != null) sfxSlider.value = savedSFX;

        // 3. Áp dụng ngay lập tức mức âm lượng đó vào Mixer
        SetMusicVolume(savedMusic);
        SetSFXVolume(savedSFX);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOverMenu.activeSelf) return;

            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void SetMusicVolume(float volume)
    {
        if (volume <= 0) volume = 0.0001f; // Tránh lỗi Log10
        mainMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolSave", volume); // Lưu lại khi người chơi kéo
    }

    public void SetSFXVolume(float volume)
    {
        if (volume <= 0) volume = 0.0001f;
        mainMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolSave", volume); // Lưu lại khi người chơi kéo
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}