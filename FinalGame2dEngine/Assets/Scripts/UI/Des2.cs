using UnityEngine;
using System.Collections;
public class Des2 : MonoBehaviour
{
    [SerializeField] private GameObject des2Screen;
    [SerializeField] private AudioClip tele;
    private bool activated2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated2)
        {
            
            StartCoroutine(ResumeGame1());
        }
    }
    IEnumerator ResumeGame1()
    {
        SoundManager.Instance.PlaySound(tele);
        yield return new WaitForSeconds(2.5f);
        des2Screen.SetActive(true);
        Time.timeScale = 0;
        activated2 = true;

    }
}
