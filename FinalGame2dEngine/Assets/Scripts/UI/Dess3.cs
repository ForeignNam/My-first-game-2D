using UnityEngine;
using System.Collections;
public class Dess3 : MonoBehaviour
{
    [SerializeField] private GameObject des3Screen;
    [SerializeField] private AudioClip des;
    private bool activated;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            StartCoroutine(ResumeGame1());
        }
    }
    IEnumerator ResumeGame1()
    {
        SoundManager.Instance.PlaySound(des);
        yield return new WaitForSeconds(2f);
        des3Screen.SetActive(true);
        Time.timeScale = 0;
        activated = true;
    }
}
