using UnityEngine;

public class button1 : MonoBehaviour
{
    [SerializeField] private Ufo ufo;
    [SerializeField] private AudioClip butonsound;
    private Animator anim;

    private bool isPressed;
    private void Awake()
    {
         anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPressed) return;
        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(butonsound);
            isPressed = true;
            anim.SetTrigger("activated");
            if (ufo != null)
            {
                ufo.Count();
            }
        }
    }
}
