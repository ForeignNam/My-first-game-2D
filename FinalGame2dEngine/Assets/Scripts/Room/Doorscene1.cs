using UnityEngine;
using System.Collections;
public class Doorscene1 : MonoBehaviour
{
    
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject heart;
    [SerializeField] private float waitTime;
    [SerializeField] private AudioClip doorSound;
    private Animator anim;
    private int count;
    private bool canopen;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
       
        count = 0;
        canopen = false;

       
        if (anim != null)
        {
            anim.Rebind(); 
            anim.ResetTrigger("open");
            anim.ResetTrigger("close");
        }
    }
    public void CountCheckpoint()
    {
        count++;
        if(count >= 3)
        {
            SoundManager.Instance.PlaySound(doorSound);
            anim.SetTrigger("open");
            
            heart.SetActive(true);
            canopen = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && canopen)
        {

            StartCoroutine(TeleportRoutine(collision.transform));
        }
       
    }
    IEnumerator TeleportRoutine(Transform playerTransform)
    {
        
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
