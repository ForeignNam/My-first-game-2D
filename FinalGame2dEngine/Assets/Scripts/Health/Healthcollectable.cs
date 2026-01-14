using UnityEngine;

public class Healthcollectable : MonoBehaviour
{
    [SerializeField] private float healthAmount;
    [SerializeField] private AudioClip healthCollectSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
              SoundManager.Instance.PlaySound(healthCollectSound);
            collision.GetComponent<Health>().AddHealth(healthAmount);
           gameObject.SetActive(false);
        }
    }

}
