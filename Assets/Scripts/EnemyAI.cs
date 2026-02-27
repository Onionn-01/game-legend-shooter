using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;

    void Start()
    {
        // Tìm đối tượng có tên là "player" trong Scene
        GameObject p = GameObject.Find("player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Di chuyển hướng về phía Player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Tùy chọn: Xoay mặt về phía Player
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }
}