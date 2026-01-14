using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackcooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField]  private AudioClip FireattackSound;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer= Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();   
        playerMovement = GetComponent<PlayerMovement>();    
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackcooldown && playerMovement.canAttack())
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        SoundManager.Instance.PlaySound(FireattackSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;
         fireballs[FindFireball()].transform.position = firePoint.position;
         fireballs[FindFireball()].GetComponent<Projecttile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    private int FindFireball()
    {

        for (int i = 0; i < fireballs.Length; i++)
        {
         if(!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

}
