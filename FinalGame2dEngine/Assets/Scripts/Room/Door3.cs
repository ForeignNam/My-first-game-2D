using UnityEngine;
using System.Collections;
public class Door3 : Enemyscene3
{
    private Animator anim;
    [SerializeField] private float waitTime ;
    [SerializeField] private Transform destination;
    [SerializeField] private AudioClip doorSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ForceOpen()
    {
        if (anim != null)
        {
            anim.SetTrigger("open");
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isOpen)
        {
           StartCoroutine(TeleportRoutine(collision.transform));
        }
    }
    IEnumerator TeleportRoutine(Transform playerTransform)
    {
        
        SoundManager.Instance.PlaySound(doorSound);
        if (anim != null) anim.SetTrigger("open");

        yield return new WaitForSeconds(waitTime);

        if (destination != null)
        {
            playerTransform.position = destination.position;
            yield return new WaitForSeconds(waitTime);
            anim.SetTrigger("close");
        }
    }
}

