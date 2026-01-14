using UnityEngine;

public class PlayerRespown : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }
    public void CheckRespawn()
    {
        if (uiManager != null)
        {
            uiManager.GameOver();
        }
        else
        {
            Debug.LogError("Chưa tìm thấy UIManager! Hãy kiểm tra lại scene.");
        }
        if (currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position;
        }
        playerHealth.Respawn();


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "CheckPoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.Instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}

