using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Bow Visuals")]
    public GameObject bowNormal;     // Kéo Object chứa ảnh cung thường vào đây
    public GameObject bowActive;     // Kéo Object chứa ảnh cung có tên vào đây
    public float effectDuration = 0.1f; // Thời gian hiện cung có tên (0.1 giây là vừa đẹp)

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Transform bodySprite;    // Kéo Object con chứa hình ảnh nhân vật vào đây

    [Header("Shooting Settings")]
    public GameObject bulletPrefab; // Mũi tên
    public Transform firePoint;     // Điểm đầu mũi tên
    public Transform gunPivot;      // Tâm xoay của cái cung
    public float bulletForce = 20f;
    public int bulletCount = 1;
    public float spreadAngle = 10f;

    private Vector2 moveInput;
    private Vector2 mousePos;

    void Update()
    {
        // Nếu game đang Pause thì không làm gì cả
        if (Time.timeScale == 0) return;

        // 1. Nhận input di chuyển
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // 2. Lấy vị trí chuột
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 3. Xử lý bắn
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // 4. Xử lý xoay vũ khí và lật nhân vật
        HandleRotationAndFlip();
    }

    void FixedUpdate()
    {
        // Di chuyển bằng Rigidbody để không bị xuyên tường
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }

    void HandleRotationAndFlip()
    {
        if (gunPivot != null)
        {
            // 1. XOAY CUNG: Tính toán góc chuẩn xác theo chuột
            Vector2 lookDir = mousePos - (Vector2)gunPivot.position;
            // -90f nếu mũi tên hướng lên trên trong file gốc, 0f nếu hướng sang phải
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            // Chỉ thay đổi góc xoay, KHÔNG chạm vào localScale
            gunPivot.rotation = Quaternion.Euler(0, 0, angle);

            // 2. LẬT NHÂN VẬT: Chỉ lật cái ảnh Body_Sprite
            if (mousePos.x > transform.position.x)
            {
                bodySprite.localScale = new Vector3(1, 1, 1); // Nhìn phải
            }
            else
            {
                bodySprite.localScale = new Vector3(-1, 1, 1); // Nhìn trái
            }

            // CHỐT: Không viết thêm bất kỳ dòng nào đụng đến gunPivot.localScale ở đây
        }
    }

    void Shoot()
    {
        // --- HIỆU ỨNG ĐỔI CUNG ---
        if (bowNormal != null && bowActive != null)
        {
            bowNormal.SetActive(false); // Ẩn cung thường
            bowActive.SetActive(true);  // Hiện cung có tên
            Invoke("ResetBow", effectDuration); // Hẹn giờ 0.1s sau thì đổi lại
        }

        // --- LOGIC BẮN TÊN (Giữ nguyên của bạn) ---
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

    // --- THÊM HÀM NÀY VÀO CUỐI SCRIPT ---
    void ResetBow()
    {
        bowNormal.SetActive(true);
        bowActive.SetActive(false);
    }
}