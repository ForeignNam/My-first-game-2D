using UnityEngine;

public class ProtectCircle : MonoBehaviour
{
    [SerializeField] private GameObject Shield;
    public bool isBlocking;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shield.SetActive(true);
            isBlocking = true;
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.F))
            {

            
                Shield.SetActive(false);
                isBlocking = false;
            }

        }

    }
}
