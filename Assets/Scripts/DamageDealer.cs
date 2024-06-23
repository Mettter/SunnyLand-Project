using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 10;
    public float knockbackForce = 10f;
    public float stunDuration = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
            playerHealth.TakeDamage(damageAmount, knockbackDirection * knockbackForce, stunDuration);
        }
    }
}
