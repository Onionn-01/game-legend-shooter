using UnityEngine;

public class BigChunkEnemy : EnemyAI
{
    protected override void Start() // Dùng override để ghi đè hàm Start của cha
    {
        // 1. Gọi Start của cha để lấy Rigidbody và tìm Player
        base.Start();

        // 2. Chỉnh thông số riêng cho con Big Chunk này
        transform.localScale = new Vector3(2.5f, 2.5f, 1f); // To ra
        speed = 1f;                                         // Chậm lại
        maxHealth = 500f;                                    // Trâu hơn
        currentHealth = maxHealth;
        damage = 50f;                                       // Đấm đau hơn
    }
}