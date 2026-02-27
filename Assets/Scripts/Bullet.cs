using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            // 1. Tìm script EnemyAI trên quái bị trúng đạn
            EnemyAI enemy = hitInfo.GetComponent<EnemyAI>();

            if (enemy != null)
            {
                // 2. Lấy sát thương đạn từ Player (Bạn cần đảm bảo Player có script PlayerStats)
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    float dmg = player.GetComponent<PlayerStats>().bulletDamage;
                    enemy.TakeDamage(dmg); // Trừ máu quái
                }
            }

            // 3. Xóa viên đạn sau khi trúng
            Destroy(gameObject);
        }

        if (hitInfo.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}