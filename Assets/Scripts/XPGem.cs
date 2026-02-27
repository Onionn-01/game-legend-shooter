using UnityEngine;

public class XPGem : MonoBehaviour
{
    public int xpAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Cộng XP cho Player
            collision.GetComponent<PlayerStats>().GainXP(xpAmount);

            // Biến mất sau khi nhặt
            Destroy(gameObject);
        }
    }
}