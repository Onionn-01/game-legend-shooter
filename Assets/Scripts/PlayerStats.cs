using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Để load lại game khi chết

public class PlayerStats : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;

    [Header("XP & Level Settings")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public Slider xpSlider;
    public TextMeshProUGUI levelText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player đã tử trận!");
        // Load lại cảnh hiện tại (Restart game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    void LevelUp()
    {
        level++;
        currentXP -= xpToNextLevel;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f);
        // ... code cũ của bạn ...

        // Tăng sức mạnh cho Player khi lên cấp
        meleeDamage += 5f;
        bulletDamage += 10f;
        maxHealth += 20f;

        // Bonus: Hồi đầy máu khi lên cấp cho "sướng"
        currentHealth = maxHealth;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (xpSlider != null)
        {
            xpSlider.maxValue = xpToNextLevel;
            xpSlider.value = currentXP;
        }
        if (levelText != null) levelText.text = "LV: " + level;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    [Header("Attack Settings")]
    public float meleeDamage = 20f; // Sát thương khi quái chạm vào người
    public float bulletDamage = 50f; // Sát thương của đạn (dùng cho bước sau)

   
}