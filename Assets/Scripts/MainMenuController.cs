using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio; // Bắt buộc có để dùng AudioMixer
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject optionsPanel;

    [Header("Audio Settings")]
    public AudioMixer mainMixer; // Kéo file MainMixer vào đây

    public Slider musicSlider; // Kéo Slider Music vào đây
    public Slider sfxSlider;   // Kéo Slider SFX vào đây

    void Start()
    {
        // TẢI DỮ LIỆU KHI MỞ MENU
        // Nếu chưa có dữ liệu (lần đầu chơi), mặc định là 0.75f (75%)
        float savedMusic = PlayerPrefs.GetFloat("MusicVolSave", 0.75f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolSave", 0.75f);

        // Gán giá trị vào Slider để nút trượt nhảy đúng vị trí
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;

        // Áp dụng luôn vào Mixer
        SetMusicVolume(savedMusic);
        SetSFXVolume(savedSFX);
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Game_Screen");
    }

    // Mở popup
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    // Đóng popup (Gán cho nút Close)
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    // Hàm chỉnh nhạc nền (Music)
    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);

        // LƯU DỮ LIỆU
        PlayerPrefs.SetFloat("MusicVolSave", volume);
    }

    public void SetSFXVolume(float volume)
    {
        mainMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);

        // LƯU DỮ LIỆU
        PlayerPrefs.SetFloat("SFXVolSave", volume);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}