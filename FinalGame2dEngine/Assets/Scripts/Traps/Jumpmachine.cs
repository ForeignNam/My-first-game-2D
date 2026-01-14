using UnityEngine;
using System.Collections;
public class Jumpmachine : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    [SerializeField] private float stunTime ;
    [SerializeField] private LayerMask targetLayer;

    [Header("Vùng kiểm tra (Check Area)")]
    [SerializeField] private Transform checkPoint;
    [SerializeField] private Vector2 checkSize = new Vector2(1f, 0.5f);

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
   public void ActivatedLoxo()
    {
        
          
        Collider2D[] targets = Physics2D.OverlapBoxAll(checkPoint.position, checkSize, 0f, targetLayer);
        foreach(Collider2D target in targets)
        {
            StartCoroutine(PushAndReset(target.gameObject));
        }
       
            

    }
    IEnumerator PushAndReset(GameObject enemy)
    {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        Spikehead spikehead = enemy.GetComponent<Spikehead>();
        if (rb != null)
        {
            if (spikehead != null) spikehead.enabled = false;


            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Dynamic; // Trở thành vật rắn
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Chống xuyên tường
            rb.gravityScale = 2f;
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            
            yield return new WaitForSeconds(stunTime);
            if (rb.bodyType == RigidbodyType2D.Kinematic)
            {
                yield break; // Thoát khỏi hàm này luôn
            }
            if (enemy != null && rb.bodyType != RigidbodyType2D.Kinematic)
            {
                // Phanh gấp lại
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;

                // Tắt trọng lực để nó lơ lửng trở lại (như lúc chưa bị đẩy)
                rb.gravityScale = 0f;
                // rb.bodyType = RigidbodyType2D.Kinematic; // Có thể bật lại Kinematic nếu muốn

                // Bật lại trí tuệ để nó tiếp tục đuổi theo Player
                if (spikehead != null) spikehead.enabled = true;

                Debug.Log("Spikehead đã hồi phục và đang đuổi theo bạn!");
            }
        }
       




    }
    private void OnDrawGizmosSelected()
    {
        if (checkPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(checkPoint.position, checkSize);
        }
    }

}
