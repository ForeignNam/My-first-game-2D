using UnityEngine;

public class Climb : MonoBehaviour
{
    [SerializeField] private float climbSpeed ;
    [SerializeField] private LayerMask ladderground ;
    
    private Rigidbody2D rb;
    private bool isclimbing;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isclimbing = true;
            rb.gravityScale = 0f;
            Physics2D.IgnoreLayerCollision(8, 15, true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isclimbing = false;
            rb.gravityScale =7f;
            Physics2D.IgnoreLayerCollision(8, 15, false);
        }
    }
    private void FixedUpdate()
    {
        if(isclimbing)
        {
            float inputVertical = Input.GetAxisRaw("Vertical");
            Physics2D.IgnoreLayerCollision(8, 15, true);
            if (inputVertical <= 0.0001)
            {
                rb.gravityScale = 0f;
               
            }
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, inputVertical * climbSpeed);
        }
    }
}


