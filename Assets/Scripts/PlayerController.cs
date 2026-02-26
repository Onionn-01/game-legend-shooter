using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 mousePos;

    void Update()
    {
        // 1. L?y input di chuy?n
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // 2. L?y v? tr� chu?t trong kh�ng gian game
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Di chuy?n nh�n v?t
        rb.linearVelocity = moveInput.normalized * moveSpeed;

        // Xoay nh�n v?t h??ng v? ph�a chu?t
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}