using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header ("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;

    [Header("SFX")]
    [SerializeField] private AudioClip spikeheadAttackSound;

    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //Moving spikehead to destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
            checkTimer += Time.deltaTime;
        if (checkTimer > checkDelay)
            CheckForPlayers();
    }


    private void CheckForPlayers()
    {
        CalculateDirections();

        for (int i = 0; i< directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer| groundLayer);
            if (hit.collider != null && !attacking)
            {
                if(((1<< hit.collider.gameObject.layer) & playerLayer) != 0)
                {
                    Stop();
                    SoundManager.Instance.PlaySound(spikeheadAttackSound);
                    attacking = true;
            destination = directions[i];
            checkTimer = 0;

        }
                
            }
        }

    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;

    }

    public void Stop()
    {
        destination = Vector3.zero;
        attacking = false;  


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            
            Stop();     
            return; 
        }
        if (collision.CompareTag("button"))
           Stop();  

        
        base.OnTriggerEnter2D(collision);
    }
}
