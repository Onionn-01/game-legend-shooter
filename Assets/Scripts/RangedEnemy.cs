using UnityEngine;

public class RangedEnemy : EnemyAI
{
    [Header("Ranged Settings")]
    public GameObject bulletPrefab;
    public float stopDistance = 5f;
    public float fireRate = 2f;
    private float nextFireTime;

    void FixedUpdate()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > stopDistance)
            {
                // Nếu còn xa thì di chuyển lại gần
                Vector2 direction = (player.position - transform.position).normalized;
                rb.linearVelocity = direction * speed; // rb giờ đã truy cập được nhờ 'protected'
            }
            else
            {
                // Nếu đủ gần thì dừng lại
                rb.linearVelocity = Vector2.zero;

                // Logic bắn đạn
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 shootDir = (player.position - transform.position).normalized; // player đã truy cập được

        Rigidbody2D brb = bullet.GetComponent<Rigidbody2D>();
        if (brb != null)
        {
            brb.linearVelocity = shootDir * 10f;
        }
    }
}