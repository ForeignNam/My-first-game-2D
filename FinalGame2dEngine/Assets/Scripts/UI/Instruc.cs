using UnityEngine;
using System.Collections;
public class Instruc : MonoBehaviour
{
    [SerializeField] private GameObject instructionScreen;
    [SerializeField] private float waittime;
    [SerializeField] private PlayerAttack attack;
    
    private bool active;
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if(collision.CompareTag("Player") && !active)
            {
               StartCoroutine(ResumeGame());
        }
          

    }
    public void EnabaleAttack()
    {
        attack.enabled = true;
    }
    IEnumerator ResumeGame()
    {
        yield return new WaitForSeconds(waittime);
        instructionScreen.SetActive(true);
        Time.timeScale = 0;
        active = true;
    }

}
