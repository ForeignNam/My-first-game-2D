using UnityEngine;
using UnityEngine.UI;
public class Volumetext : MonoBehaviour
{
   private Text volumeText;
    [SerializeField] private string volumName;
    [SerializeField] private string textIntro;
    private void Start()
    {
         volumeText = GetComponent<Text>();
    }
    private void Update()
    {
        UpdateVolume();
    }
    private void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumName) * 100;
        volumeText.text = textIntro + volumeValue.ToString();
    }
}
