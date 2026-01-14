using UnityEngine;

public class checkpointbock : MonoBehaviour
{
    public bool canopen = false;
    [SerializeField] private GameObject checkpointEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !canopen)
        {
            if(checkpointEffect != null)
                checkpointEffect.GetComponent<Animator>().SetTrigger("open");
            canopen = true;
        }
    }

}
