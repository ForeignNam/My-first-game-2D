using UnityEngine;
using System.Collections;
public class Firetrap : MonoBehaviour
{

    [SerializeField] private float damage;
    [Header("Firetrap Timers")]
    [SerializeField] private float activedelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("Firetrap Sound")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" )
        {
           
            if (!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }
            if(active)
            {
                ProtectCircle shield = collision.GetComponent<ProtectCircle>();
                if (shield != null && shield.isBlocking)
                {
                    
                    return;
                }
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRend.color = Color.red;

        yield return new WaitForSeconds(activedelay);
        SoundManager.Instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        triggered = false;
        active = false;
        anim.SetBool("activated", false);

    }
}
