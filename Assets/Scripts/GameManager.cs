using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Dùng để các script khác gọi nhanh

    [Header("UI Panels")]
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    private bool isPaused = false;

    void Awake()
    {
        // Tạo Singleton để dễ truy cập từ script khác (ví dụ: GameManager.instance.GameOver())
        instance = this;
    }

    void Update()
    {
        // Bấm ESC để đóng/mở Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Dừng thời gian game
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Chạy lại thời gian
        isPaused = false;
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
        SceneManager.LoadScene("MainMenu"); // Tên Scene màn hình chính của bạn
    }
}