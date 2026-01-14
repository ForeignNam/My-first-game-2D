using UnityEngine;

public class Enemy_Sideway : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float movementdistance;
    [SerializeField] private bool movingleft ;
    private float leftedge;
    private float rightedge;
    private void Awake()
    {
        leftedge = transform.position.x - movementdistance;
        rightedge = transform.position.x + movementdistance;
    }


    private void Update()
    {
        if (movingleft)
        {
            if(transform.position.x > leftedge)
            {
               transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingleft = false;
            }
        }
        else
        {
            if(transform.position.x < rightedge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingleft = true;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ProtectCircle shield = collision.GetComponent<ProtectCircle>();
            if(shield != null)
                {
                if (shield.isBlocking)
                {
                    return;
                }
            }
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
