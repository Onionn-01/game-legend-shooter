using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;
    public int bulletCount = 1;      // MỚI: Số lượng tia đạn
    public float spreadAngle = 10f;  // MỚI: Độ tỏa của đạn

    private Vector2 moveInput;
    private Vector2 mousePos;

    void Update()
    {
        // Chỉ cho phép thao tác khi game không bị tạm dừng (Time.timeScale != 0)
        if (Time.timeScale == 0) return;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * moveSpeed;

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        // Tính toán góc bắt đầu để các tia đạn tỏa đều
        float startAngle = -spreadAngle * (bulletCount - 1) / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + (i * spreadAngle);
            // Tạo góc xoay cho từng tia đạn
            Quaternion bulletRotation = firePoint.rotation * Quaternion.Euler(0, 0, currentAngle);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            Rigidbody2D brb = bullet.GetComponent<Rigidbody2D>();

            // Đẩy đạn đi theo hướng "up" của chính viên đạn đó (đã bao gồm góc lệch)
            brb.AddForce(bullet.transform.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}