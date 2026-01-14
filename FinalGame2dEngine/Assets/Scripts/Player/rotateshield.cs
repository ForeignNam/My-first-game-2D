using UnityEngine;

public class rotateshield : MonoBehaviour
{
    [SerializeField] private float rotationSpeed ;
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
