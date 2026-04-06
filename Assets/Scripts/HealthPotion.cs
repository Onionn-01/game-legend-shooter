using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float healAmount = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats player = collision.GetComponent<PlayerStats>();

            if (player != null)
            {
                player.Heal(healAmount);
            }

            Destroy(gameObject);
        }
    }
}