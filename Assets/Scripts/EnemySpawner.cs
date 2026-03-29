using UnityEngine;
using System.Collections.Generic; // Cần thiết để dùng List

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject normalEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public GameObject bigChunkPrefab;

    [Header("References")]
    public Transform player;
    public SpriteRenderer floorRenderer;
    private PlayerStats playerStats; // Lưu reference để check level nhanh hơn

    [Header("Spawn Settings")]
    public float spawnRate = 2f;
    public float spawnDistance = 10f;
    private float nextSpawnTime;

    void Start()
    {
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && player != null && floorRenderer != null)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // 1. Xác định loại quái sẽ spawn dựa trên Level
        GameObject prefabToSpawn = SelectEnemyByLevel();

        // 2. Tính toán vị trí ngẫu nhiên và giới hạn trong sàn (Giữ nguyên logic cũ của bạn)
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = player.position + (Vector3)(randomDirection * spawnDistance);

        Bounds bounds = floorRenderer.bounds;
        float clampedX = Mathf.Clamp(spawnPosition.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(spawnPosition.y, bounds.min.y, bounds.max.y);
        Vector3 finalPosition = new Vector3(clampedX, clampedY, 0);

        // 3. Sinh ra quái
        GameObject enemy = Instantiate(prefabToSpawn, finalPosition, Quaternion.identity);

        // 4. Thiết lập chỉ số (Scale theo level)
        EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();
        if (enemyScript != null && playerStats != null)
        {
            int pLevel = playerStats.level;

            // Chỉ số cơ bản tăng dần theo level player
            float scaledHP = 20f + (pLevel * 10f);
            float scaledDmg = 5f + (pLevel * 2f);

            // Bonus thêm cho con Big Chunk nếu nó xuất hiện
            if (prefabToSpawn == bigChunkPrefab)
            {
                scaledHP *= 3f; // Máu gấp 3 quái thường
                scaledDmg *= 2f;
            }

            enemyScript.SetEnemyStats(scaledHP, scaledDmg);
        }
    }

    GameObject SelectEnemyByLevel()
    {
        int currentLevel = (playerStats != null) ? playerStats.level : 1;

        // Tạo một danh sách các quái "hợp lệ" tại level hiện tại
        List<GameObject> possibleEnemies = new List<GameObject>();

        // Luôn luôn có quái thường
        possibleEnemies.Add(normalEnemyPrefab);

        // Level 5 trở lên mới có quái xa
        if (currentLevel >= 5)
        {
            possibleEnemies.Add(rangedEnemyPrefab);
        }

        // Level 8 trở lên mới có quái to
        if (currentLevel >= 8)
        {
            possibleEnemies.Add(bigChunkPrefab);
        }

        // Chọn ngẫu nhiên 1 con trong danh sách những con đã đạt điều kiện level
        int randomIndex = Random.Range(0, possibleEnemies.Count);
        return possibleEnemies[randomIndex];
    }
}