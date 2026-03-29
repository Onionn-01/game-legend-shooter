using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 2f;
    public float maxHealth;
    public float currentHealth;
    public float damage;

    [Header("Drops")]
    public GameObject xpPrefab;

    protected Transform player;  // Đổi private thành protected
    protected Rigidbody2D rb;     // Đổi private thành protected // Thêm Rigidbody để điều khiển vật lý

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy component Rigidbody

        // Đảm bảo quái không bị xoay tròn khi va chạm
        rb.freezeRotation = true;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    public void SetEnemyStats(float hp, float dmg)
    {
        maxHealth = hp;
        currentHealth = hp;
        damage = dmg;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (xpPrefab != null)
        {
            Instantiate(xpPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    // Dùng FixedUpdate thay cho Update để xử lý di chuyển vật lý mượt hơn
    void FixedUpdate()
    {
        if (player != null)
        {
            // 1. Tính toán hướng về phía Player
            Vector2 direction = (player.position - transform.position).normalized;

            // 2. Gán vận tốc trực tiếp (linearVelocity dành cho Unity 6)
            // Nếu bạn dùng bản thấp hơn, hãy đổi thành rb.velocity
            rb.linearVelocity = direction * speed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats pStats = collision.gameObject.GetComponent<PlayerStats>();
            if (pStats != null)
            {
                pStats.TakeDamage(damage * Time.deltaTime);
                TakeDamage(pStats.meleeDamage * Time.deltaTime);
            }
        }
    }
}