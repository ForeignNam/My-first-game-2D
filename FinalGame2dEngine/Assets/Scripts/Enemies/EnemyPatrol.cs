using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [Header("Idle Behaviers")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    [Header("Enemy Animation")]
    [SerializeField]  private Animator anim;
    
    private void Awake()
    {
       
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
 
    private void Update()
    {
        if (movingLeft) {
            if (enemy.transform.position.x >= leftEdge.position.x)
            {

                MovementDirection(-1);
            }
            else
            {
                ChangeDirection();
            }

        }
        else
        {
            if (enemy.transform.position.x <= rightEdge.position.x)
            {
                MovementDirection(1);
            }
            else
                ChangeDirection();


        }
  
    }
           
private void ChangeDirection()
{
       
        
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if(idleTimer >= idleDuration)
            movingLeft = !movingLeft;

}
private void MovementDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        enemy.position= new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);



    }
}
