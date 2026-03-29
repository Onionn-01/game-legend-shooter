using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 5f;

    void Start()
    {
        // Tự hủy sau một khoảng thời gian để tránh rác bộ nhớ
        Destroy(gameObject, lifeTime);
    }

    // Hàm này chạy khi đạn chạm vào cái gì đó
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Kiểm tra xem có chạm vào Player không
        if (collision.CompareTag("Player"))
        {
            // 2. Lấy script PlayerStats của người chơi
            PlayerStats pStats = collision.GetComponent<PlayerStats>();
            if (pStats != null)
            {
                pStats.TakeDamage(damage); // Gây sát thương
            }

            // 3. Hủy viên đạn sau khi trúng đích
            Destroy(gameObject);
        }

        // Nếu chạm vào tường (giả sử tường có Tag là Wall) cũng hủy đạn
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}