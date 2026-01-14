using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform leftPoint;
    private bool movingRight;
    [SerializeField] private Animator anim;  
    
   
 
    private bool isplayeronboard;
    private bool isActivated;
    private int count;
    private void Start()
    {
       movingRight = true;
        isActivated = false;
        isplayeronboard = false;
        count = 0;
    }
    private void Update()
    {
        if(isActivated && isplayeronboard)
        {
            if (movingRight)
            {
                if (transform.position.x <= rightPoint.position.x)
                {
                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                 ChangeDirection();
                }
            }
            else
            {
                if(transform.position.x >= leftPoint.position.x)
                {
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    ChangeDirection();
                }
            }
           
            
        }
    }
    private void ChangeDirection()
    {
        movingRight = !movingRight;

    }
    public void Count()
    {
        count++;
        if (count >= 2)
        {
            isActivated = true;
            anim.SetTrigger("activated");

        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player on board");
            isplayeronboard = true;
            collision.transform.SetParent(this.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isplayeronboard = false;
            collision.transform.SetParent(null);
        }
    }

}
