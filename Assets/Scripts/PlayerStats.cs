using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("UI & Panel")]
    public GameObject levelUpPanel; // Kéo LevelUpPanel vào đây

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

    [Header("Attack Settings")]
    public float meleeDamage = 20f;
    public float bulletDamage = 50f;

    void Start()
    {
        currentHealth = maxHealth;
        if (levelUpPanel != null) levelUpPanel.SetActive(false);
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateUI();
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        GameManager.instance.GameOver(); // Gọi hiện bảng chết thay vì load lại ngay
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

        // Dừng game và hiện bảng
        Time.timeScale = 0f;
        if (levelUpPanel != null) levelUpPanel.SetActive(true);

        UpdateUI();
    }

    // --- CÁC HÀM CHO NÚT BẤM (BUTTON ON CLICK) ---

    public void BtnUpgradeMoveSpeed()
    {
        GetComponent<PlayerController>().moveSpeed += 1f;
        ResumeGame();
    }

    public void BtnUpgradeBulletCount()
    {
        GetComponent<PlayerController>().bulletCount += 1;
        ResumeGame();
    }

    public void BtnUpgradeHealth()
    {
        maxHealth += 20f;
        currentHealth = maxHealth;
        UpdateUI();
        ResumeGame();
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        if (levelUpPanel != null) levelUpPanel.SetActive(false);
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
}