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
        // Công thức logarit để âm thanh giảm đều tai hơn
        mainMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
    }

    // Hàm chỉnh hiệu ứng (SFX)
    public void SetSFXVolume(float volume)
    {
        mainMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}