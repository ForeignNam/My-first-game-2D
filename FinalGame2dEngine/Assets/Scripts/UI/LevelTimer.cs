using UnityEngine;
using TMPro;
public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool isRunning;

    private void Start()
    {
        
        isRunning = true;
        elapsedTime = 0f;
    }
    private void Update()
    {
        if (isRunning)
        {
           
            elapsedTime += Time.deltaTime;

           
            UpdateTimerUI(elapsedTime);
        }
    }
    private void UpdateTimerUI(float timeToDisplay)
    {
        timeToDisplay += 1; 

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay % 1) * 100;

       
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
    public float StopTimer()
    {
        isRunning = false;
        return elapsedTime;

    }
}
