using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] private GameObject Arrowtrap;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") )
        {
            Arrowtrap.SetActive(true);
            
        }
       
    }
    
}
