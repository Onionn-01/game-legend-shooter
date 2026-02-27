using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float lifeTime = 2f; // Thời gian đạn tồn tại trước khi tự hủy

    void Start()
    {
        // Tự hủy sau một khoảng thời gian để tránh làm nặng máy
        Destroy(gameObject, lifeTime);
    }

    // Hàm này kích hoạt khi Collider của đạn chạm vào một Collider khác
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Kiểm tra xem vật bị chạm có Tag là "Enemy" không
        if (hitInfo.CompareTag("Enemy"))
        {
            // 1. Xóa kẻ địch
            Destroy(hitInfo.gameObject);

            // 2. Xóa chính viên đạn
            Destroy(gameObject);

            Debug.Log("Đã tiêu diệt kẻ địch!");
        }

        // Nếu bạn muốn đạn chạm vào tường (Wall) cũng biến mất, 
        // hãy tạo Tag "Wall" cho các khối tường và dùng lệnh tương tự:
        /*
        if (hitInfo.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        */
    }
}