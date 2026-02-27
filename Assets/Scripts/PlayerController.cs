using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab; // Kéo Prefab viên đạn vào đây
    public Transform firePoint;     // Kéo đối tượng FirePoint vào đây
    public float bulletForce = 20f;

    private Vector2 moveInput;
    private Vector2 mousePos;

    void Update()
    {
        // 1. Lấy input di chuyển
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // 2. Lấy vị trí chuột
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 3. Logic bắn đạn (Nhấn chuột trái)
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật
        rb.linearVelocity = moveInput.normalized * moveSpeed;

        // Xoay nhân vật về hướng chuột
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        // Tạo bản sao viên đạn tại vị trí và góc xoay của FirePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Lấy Rigidbody2D của viên đạn để đẩy nó đi
        Rigidbody2D brb = bullet.GetComponent<Rigidbody2D>();
        brb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}