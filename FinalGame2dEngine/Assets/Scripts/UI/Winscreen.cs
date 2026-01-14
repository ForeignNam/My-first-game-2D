using UnityEngine;
using TMPro;
public class Winscreen : MonoBehaviour
{
    
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI bestTimeText;
    public GameObject winPanelObject;

   
    private string saveKey = "Level1_BestTime";

 
    public void ShowResult(float finalTime)
    {
       
        winPanelObject.SetActive(true);

        
        currentTimeText.text = "Time: " + FormatTime(finalTime);

       
        float bestTime = PlayerPrefs.GetFloat(saveKey, float.MaxValue);

        
        if (finalTime < bestTime)
        {
            bestTime = finalTime; 
            PlayerPrefs.SetFloat(saveKey, bestTime);
            PlayerPrefs.Save(); 

        }

        
        bestTimeText.text = "Record: " + FormatTime(bestTime);
    }

    
    string FormatTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float milliseconds = (time % 1) * 100;
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
