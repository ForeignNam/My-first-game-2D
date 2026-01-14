using UnityEngine;

public class Groundmovement : MonoBehaviour
{
    [SerializeField]private float speed ;
    [SerializeField] private float movementdistanceup;
    [SerializeField] private float movementdistancedown;
    [SerializeField] private GameObject arowtrap;
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject light1;
   

    private bool hasactivated;
    private float upedge;
    private float downedge;
    [SerializeField] private bool movingup;
    private Animator anim;
    
    [SerializeField] private float timeduration;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        upedge = transform.position.y + movementdistanceup;
        downedge = transform.position.y - movementdistancedown;
    }
    private void FixedUpdate()
    {
        if(movingup)
        {
            if(transform.position.y < upedge)
            {
               transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingup = false;
            }
        }
        else
        {
            if(transform.position.y > downedge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingup = true;
            }

        }
       

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !hasactivated )
        {
          foreach(ContactPoint2D point in collision.contacts)
          {
              if(point.normal.y < -0.5f)
              {
                    arowtrap.SetActive(true);
                    anim.SetBool("activated",true);
                    light.SetActive(true);
                    light.GetComponent<Animator>().SetTrigger("activated");
                    light1.SetActive(true);
                    light1.GetComponent<Animator>().SetTrigger("activated");
                    hasactivated = true;
                    
                    break;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            hasactivated = false;
            anim.SetBool("activated",false);
           
        }
    }
    

}
