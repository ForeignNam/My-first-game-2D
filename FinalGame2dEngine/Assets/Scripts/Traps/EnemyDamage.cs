using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") )
        {
            ProtectCircle shield = collision.GetComponent<ProtectCircle>();
            if(shield != null && shield.isBlocking)
            {
           
                return;
            }
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
