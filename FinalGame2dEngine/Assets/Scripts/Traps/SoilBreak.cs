using UnityEngine;
using System.Collections;
public class SoilBreak : MonoBehaviour
{
    private Animator anim;
   
    [SerializeField] private float resetTime;
    
    private bool isbreaking ;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& !isbreaking)
        {
           StartCoroutine(BreakSoil());
        }
    }
    IEnumerator BreakSoil()
    {
        isbreaking = true;
        anim.SetTrigger("run");
        yield return new WaitForSeconds(resetTime);
        anim.SetTrigger("bom");
      
    }
    public void Destroy()
    {
      Destroy(gameObject);
    }

}


