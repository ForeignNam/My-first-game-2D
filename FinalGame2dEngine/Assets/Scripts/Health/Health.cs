using UnityEngine;
using System.Collections;
public class Health : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private float startinghealth;
    private Animator anim;
    private bool dead;
    public  float currenthealth {get; private set; }

    [Header("iFrames")]
    [SerializeField]  private float iFramesDuration;
    [SerializeField]  private int numberofFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField]  private Behaviour[] components;
    private bool invulnerable;
    [Header("Death sound")]
    [SerializeField]  private AudioClip deathSound;
    [SerializeField]  private AudioClip hurtsound;

    private void Awake()
    {
        currenthealth = startinghealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currenthealth = Mathf.Clamp(currenthealth - _damage, 0, startinghealth);
 
       if(currenthealth > 0)
        {
            anim.SetTrigger("hurt");
           StartCoroutine(IFrames());
            SoundManager.Instance.PlaySound(hurtsound);
        }
        else
          {
            if (!dead)
            {
                
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                anim.SetTrigger("grounded");
                anim.SetTrigger("die");
                dead = true;
                SoundManager.Instance.PlaySound(deathSound);
                StartCoroutine(ExecuteGameOverAfterTime(1.5f));
            }
           
        }
    }
    IEnumerator ExecuteGameOverAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        
        if (GetComponent<PlayerRespown>() != null)
        {
            GetComponent<PlayerRespown>().CheckRespawn();
        }
    }
    public void AddHealth(float _value)
    {
        currenthealth = Mathf.Clamp(currenthealth + _value, 0, startinghealth);
    }
    public void Respawn()
    {
        dead = false;
        AddHealth(startinghealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(IFrames());
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }
    private IEnumerator IFrames()
    {
      
        for (int i = 0; i < numberofFlashes; i++)
        {
            invulnerable = true;
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/(numberofFlashes*2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberofFlashes * 2));
            invulnerable = false;
        }

      
    
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
