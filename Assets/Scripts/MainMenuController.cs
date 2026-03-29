using UnityEngine;
using UnityEngine.SceneManagement; // Bắt buộc có dòng này để chuyển cảnh

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Chú ý: "GameScene" phải trùng tên với tên Scene bạn đặt cho màn chơi bắn súng
        SceneManager.LoadScene("Game_Screen");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit(); // Thoát game (chỉ có tác dụng khi đã xuất file .exe)
    }
}