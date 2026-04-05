using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 2f;
    public float maxHealth;
    public float currentHealth;
    public float damage;

    [Header("Visuals")]
    public Transform visualSprite; // Kéo Object con chứa Sprite của quái vào đây

    [Header("Drops")]
    public GameObject xpPrefab;

    protected Transform player;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.enemyDieSound);
        }
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // 1. Di chuyển về phía Player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            // 2. XOAY MẶT VỀ PHÍA PLAYER (MỚI)
            HandleFlip();
        }
    }

    void HandleFlip()
    {
        if (visualSprite != null)
        {
            // Nếu Player nằm bên phải quái
            if (player.position.x > transform.position.x)
            {
                visualSprite.localScale = new Vector3(1, 1, 1);
            }
            // Nếu Player nằm bên trái quái
            else
            {
                visualSprite.localScale = new Vector3(-1, 1, 1);
            }
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