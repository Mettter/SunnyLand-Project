using UnityEngine;

public class EnemyDMGdealer : MonoBehaviour
{
    public Movement move;
    public int damageAmount = 10;
    public float knockbackForce = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null && move.rb.gravityScale > 3f)
        {
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
            enemyHealth.ETakeDamage(damageAmount, knockbackDirection * knockbackForce);
            move.IsGrounded();
        }
    }
}