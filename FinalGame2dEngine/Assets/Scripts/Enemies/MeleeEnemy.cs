using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackcooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Health Playerhealth;
    [Header("Attack Sound")]
    [SerializeField] private AudioClip fireballsshound;
  
    
    private Animator anim;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(PlayerinSight())
        {
            if (cooldownTimer >= attackcooldown && Playerhealth.currenthealth> 0)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeattack");
                SoundManager.Instance.PlaySound(fireballsshound);
            }

        }
     if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerinSight();
        }

    }   


    private bool PlayerinSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right *range * transform.localScale.x * colliderDistance,new Vector3(boxCollider.bounds.size.x * range, 
            boxCollider.bounds.size.y,
            boxCollider.bounds.size.z),
            0,Vector2.left,0,playerLayer);

        if(hit.collider != null)
        {
            Playerhealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null; 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range,
            boxCollider.bounds.size.y,
            boxCollider.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if (PlayerinSight())
        {
            ProtectCircle shield = Playerhealth.GetComponent<ProtectCircle>();
            if (shield != null && shield.isBlocking)
            {
                return;
            }
            Playerhealth.TakeDamage(damage);
        }
    }
}
