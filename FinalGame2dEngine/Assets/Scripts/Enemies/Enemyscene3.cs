using UnityEngine;

public class Enemyscene3 : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyDoor3;
    [SerializeField] private GameObject door3;
    [SerializeField] private GameObject heart;
    private Door3 doorScript;
    public bool isOpen;
    private void Start()
    {
        if (door3 != null)
        {
            doorScript = door3.GetComponent<Door3>();
        }
        isOpen = false;
        heart.SetActive(false);
    }
    void Update()
    {
        if (isOpen) return;
        for (int i = 0; i < enemyDoor3.Length; i++)
        {
            if (enemyDoor3[i] != null && enemyDoor3[i].activeInHierarchy)
            {
                return;
            }
        }
        OpentheDoor3();
    }

    private void OpentheDoor3()
    {
        Debug.Log("Đã tiêu diệt hết quái! Mở cửa.");
        isOpen = true;
        heart.SetActive(true);
        if (doorScript != null)
        {
            doorScript.ForceOpen();
        }
    }
}
