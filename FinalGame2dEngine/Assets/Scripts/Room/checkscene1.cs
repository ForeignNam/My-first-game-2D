using UnityEngine;

public class checkscene1 : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Doorscene1 doorlink;
    [SerializeField] private AudioClip checkpoint;
    private bool opened;
    private void Start()
    {
       
        if (doorlink != null)
        {
           
            if (anim != null)
            {
                anim.Rebind(); 
                anim.ResetTrigger("appear");
            }
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !opened)
        {
          anim.SetTrigger("appear");
            SoundManager.Instance.PlaySound(checkpoint);
            doorlink.CountCheckpoint();
            opened = true;
        }
        else
            doorlink.enabled = false;
    }
}
