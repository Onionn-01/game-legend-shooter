using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform gunPivot;       // MỚI: Kéo Object GunPivot vào đây
    public float bulletForce = 20f;
    public int bulletCount = 1;
    public float spreadAngle = 10f;

    private Vector2 moveInput;
    private Vector2 mousePos;

    void Update()
    {
        if (Time.timeScale == 0) return;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // --- CHỈNH SỬA PHẦN XOAY TẠI ĐÂY ---
        if (gunPivot != null)
        {
            // Tính toán góc xoay dựa trên vị trí chuột và GunPivot
            Vector2 lookDir = mousePos - (Vector2)gunPivot.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            // Xoay cái GunPivot thay vì xoay Rigidbody của Player
            gunPivot.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void FixedUpdate()
    {
        // Giữ nguyên chức năng di chuyển
        rb.linearVelocity = moveInput.normalized * moveSpeed;

        // ĐÃ XÓA: rb.rotation = angle (Để nhân vật không tự xoay nữa)
    }

    void Shoot()
    {
        // Giữ nguyên chức năng bắn nhiều tia của bạn
        float startAngle = -spreadAngle * (bulletCount - 1) / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + (i * spreadAngle);
            Quaternion bulletRotation = firePoint.rotation * Quaternion.Euler(0, 0, currentAngle);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            Rigidbody2D brb = bullet.GetComponent<Rigidbody2D>();

            brb.AddForce(bullet.transform.up * bulletForce, ForceMode2D.Impulse);
        }

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.shootSound);
        }
    }
}