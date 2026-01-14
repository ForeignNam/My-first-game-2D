using UnityEngine;

public class button : MonoBehaviour
{
    [SerializeField]private Door linkedDoor;
    [SerializeField]private AudioClip buttonnn;
    private Animator anim;
    private bool isPressed = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isPressed)
        {
            isPressed = true;
            SoundManager.Instance.PlaySound(buttonnn);
            anim.SetTrigger("activated");

            if(linkedDoor != null)
            {
                linkedDoor.Countbuttonpress();
            }
            Spikehead spikehead = collision.GetComponent<Spikehead>();
            if (spikehead != null)
            {
                spikehead.enabled = false;
            }
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.bodyType = RigidbodyType2D.Kinematic;


            }
        }

    }
}

