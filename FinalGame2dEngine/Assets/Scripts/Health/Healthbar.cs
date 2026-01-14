using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerhealth;
    [SerializeField]  private Image currenthealthbar;
    [SerializeField]  private Image totalhealthbar;


    private void Start()
    {
        totalhealthbar.fillAmount = playerhealth.currenthealth / 10;
    }


    private void Update()
    {
        currenthealthbar.fillAmount = playerhealth.currenthealth / 10;
    }   
}
