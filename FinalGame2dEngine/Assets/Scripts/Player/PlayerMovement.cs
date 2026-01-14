using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float crouchspeed;
    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoCounter;
    [Header("Multiple Jumps")]
    [SerializeField] private int jumpnumber;
    private int jumpcounter;
    [Header("Wall Jumping")]
    [SerializeField] private float WalljumpX;
    [SerializeField] private float WalljumpY;



    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask walllayer;
    private bool isCrouching;
    
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private CapsuleCollider2D standing;
    [SerializeField] private CapsuleCollider2D crouch;
    private float wallJumpCooldown;
    private float horizontal;

    [Header("SFK")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");


        //Flip the player based on movement direction
        if (horizontal > 0.01f)
        {
            transform.localScale = Vector3.one;

        }
        else if (horizontal < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }


        anim.SetBool("run", horizontal != 0);
        anim.SetBool("grounded", isgrounded());
        anim.SetBool("crouch", isCrouching);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isCrouching = true;
            standing.enabled = false;
            crouch.enabled = true;
        }
        else
        {
            isCrouching = false;
            standing.enabled = true;
            crouch.enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2);

        if (onWall())
        {
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            rb.gravityScale = 7;
            float currentSpeed = isCrouching ? crouchspeed : speed;
            if (isCrouching)
            {
                rb.linearVelocity = new Vector2(horizontal * currentSpeed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(horizontal * currentSpeed, rb.linearVelocity.y);
            }
        if (isgrounded())
        {
            coyoCounter = coyoteTime;
            jumpcounter = jumpnumber;
        }
        else
        {
            coyoCounter -= Time.deltaTime;
        }

    }
}
    
   
    private void Jump()
    {

        if (coyoCounter <= 0 && !onWall() && jumpcounter <= 0) return;
        SoundManager.Instance.PlaySound(jumpSound);
         if(onWall())
            WallJump();
        else
        {
            if (isgrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);

            }
            else
            {
                if (coyoCounter > 0)
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                else
                {
                    if(jumpcounter >0)
                    {
                        
                        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                        jumpcounter--;
                    }
                }

            }
            coyoCounter = 0;
        }


    }
    private void WallJump()
    {
        rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * WalljumpX, WalljumpY));
        wallJumpCooldown = 0;
    }
 

    private bool isgrounded()
    {

        if (isCrouching)
        {
            return Physics2D.BoxCast(crouch.bounds.center, crouch.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        }
        
        else
        {
            return Physics2D.BoxCast(standing.bounds.center, standing.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        }
    }
    private bool onWall()
    {
        return Physics2D.BoxCast(standing.bounds.center, standing.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, walllayer);
         
    }
    public bool canAttack()
    {
        return horizontal == 0 && isgrounded() && !onWall() && !isCrouching;
    }   
}
