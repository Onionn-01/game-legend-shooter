using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public SpriteRenderer floorRenderer; // MỚI: Để lấy kích thước sàn

    public float spawnRate = 2f;
    public float spawnDistance = 10f;

    private float nextSpawnTime;

    void Update()
    {
        // Thêm điều kiện kiểm tra floorRenderer
        if (Time.time >= nextSpawnTime && player != null && floorRenderer != null)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // 1. Tính toán vị trí ngẫu nhiên quanh Player (giống code cũ)
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = player.position + (Vector3)(randomDirection * spawnDistance);

        // 2. Lấy giới hạn biên của Floor
        Bounds bounds = floorRenderer.bounds;

        // 3. Ép tọa độ phải nằm TRONG sàn (Clamp)
        // Nếu spawnPosition vượt quá biên sàn, nó sẽ bị kéo về đúng mép sàn
        float clampedX = Mathf.Clamp(spawnPosition.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(spawnPosition.y, bounds.min.y, bounds.max.y);

        Vector3 finalPosition = new Vector3(clampedX, clampedY, 0);

        // 4. Sinh ra Enemy tại vị trí an toàn
        Instantiate(enemyPrefab, finalPosition, Quaternion.identity);

        GameObject enemy = Instantiate(enemyPrefab, finalPosition, Quaternion.identity);
        EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();

        if (enemyScript != null)
        {
            // Lấy level hiện tại của Player
            int pLevel = player.GetComponent<PlayerStats>().level;

            // Công thức tăng chỉ số quái (Bạn có thể tùy chỉnh con số)
            float scaledHP = 20f + (pLevel * 10f);   // Mỗi level quái tăng 10 máu
            float scaledDmg = 5f + (pLevel * 2f);   // Mỗi level quái tăng 2 sát thương

            enemyScript.SetEnemyStats(scaledHP, scaledDmg);
        }
    }
}