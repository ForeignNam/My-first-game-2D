using UnityEngine;
using System.Collections;
public class des1 : MonoBehaviour
{
    [SerializeField] private GameObject des1Screen;
    private bool activated ;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            StartCoroutine(ResumeGame());
        }
    }
    IEnumerator ResumeGame()
    {
        yield return new WaitForSeconds(2f);
        des1Screen.SetActive(true);
        Time.timeScale = 0;
        activated = true;
    }
}
