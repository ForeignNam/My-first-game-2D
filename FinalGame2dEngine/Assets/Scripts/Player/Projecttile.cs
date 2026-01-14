using UnityEngine;

public class Projecttile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxdistance;
    private float direction;
    private float lifetime;
    private bool hit;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        
    }
    private void Update()
    {
        if(hit) return;
        float movementSpeed = speed * Time.deltaTime* direction;
        transform.Translate(movementSpeed, 0, 0);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
          
            Deactivate();
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision != null && collision.CompareTag("Enemy"))
        {
            
            if (collision.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(1);
            }
        }
    }
    public void SetDirection(float _direction)
    {

        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        if (speed != 0)
        {
            lifetime = maxdistance / speed;
        }
        else
        {
            lifetime = 5f; 
        }
        float localScaleX= transform.localScale.x;
        if(Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z); 
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }   
}
