using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class jumpmachine : MonoBehaviour
{
    [SerializeField] private GameObject JumpMachine;
    private bool isPlayerinRange;
    public UnityEvent OnLeverActivated;
    private bool isActivated = false;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isPlayerinRange && Input.GetKeyDown(KeyCode.E) && !isActivated)
        {
            if(JumpMachine != null)
             
            StartCoroutine(ActivateJumpMachine());

        }
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
         isPlayerinRange = true;
            Debug.Log("Player đã vào vùng, hãy bấm E");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
         isPlayerinRange = false;    
        }
    }  
    IEnumerator ActivateJumpMachine()
    {
        if(JumpMachine != null)
        {
            isActivated = true;
         
                anim.SetBool("activated", true);
           
            if(JumpMachine != null)
            {
                Animator machineAnim = JumpMachine.GetComponent<Animator>();
                if (machineAnim != null)
                {
                    machineAnim.SetBool("activated",true);
                }
            }
            OnLeverActivated.Invoke();      
  
            yield return new WaitForSeconds(1f);
            anim.SetBool("activated", false);

            if (JumpMachine != null)
            {
                Animator machineAnim = JumpMachine.GetComponent<Animator>();
                if (machineAnim != null)
                {
                    machineAnim.SetBool("activated", false);
                }
            }
            isActivated = false;
            
        }
    }
}
