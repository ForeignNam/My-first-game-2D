using UnityEngine;
using System.Collections;
public class WInning : MonoBehaviour
{
    private bool isCollected ;

    [SerializeField] private float waitTime ;
    public Winscreen winScreen;
    private Animator anim;
    [SerializeField] public AudioSource backgroundMusic;

    public LevelTimer levelTimer;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject arrowtrap;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        isCollected = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
            }
            arrowtrap.SetActive(false);
            anim.SetTrigger("activated");
            Debug.Log("Win");
            SoundManager.Instance.PlaySound(winSound);
            StartCoroutine(WaitAndPrint(waitTime));
        }
    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        player.GetComponent<Animator>().SetTrigger("win");
        yield return new WaitForSeconds(waitTime);
        isCollected = true;
        float finalTime = levelTimer.StopTimer();

        winScreen.ShowResult(finalTime);
        text.SetActive(false);
       
        Time.timeScale = 0f;

    }
}
