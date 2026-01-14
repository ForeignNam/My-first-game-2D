using UnityEngine;
using System.Collections;
public class Door : MonoBehaviour
{
    [SerializeField] private GameObject CrashStoneEffect;
    [SerializeField] private GameObject heart;
    public checkpointbock linkedCheckpoint;
    [SerializeField] private GameObject checkpoint;
    private Animator anim;
    [Header("Cài đặt Dịch chuyển")]
    [SerializeField] private Transform destination;
    [SerializeField] private float waitTime ;
    [SerializeField] private AudioClip doorrsound;
    private int count = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Countbuttonpress()
    {
        count++;
        if (count >= 3)
        {
           checkpoint.SetActive(true);
            heart.SetActive(true);
        }
        if(count >= 2)
        {
            CrashStoneEffect.SetActive(false);
        }
       
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && linkedCheckpoint != null && linkedCheckpoint.canopen)
        {
            
            StartCoroutine(TeleportRoutine(collision.transform));
        }
        else if (collision.CompareTag("Player") && !linkedCheckpoint.canopen)
        {
            Debug.Log("Cửa bị khóa! Bạn cần chạm vào Checkpoint trước.");
        }
    }

     IEnumerator TeleportRoutine(Transform playerTransform)
    {
        SoundManager.Instance.PlaySound(doorrsound);

        if (anim != null) anim.SetTrigger("open");

        
        yield return new WaitForSeconds(waitTime);

        
        if (destination != null)
        {
          
            playerTransform.position = destination.position;
            yield return new WaitForSeconds(1f); 
            anim.SetTrigger("close");
        }
        
        
    }
}
