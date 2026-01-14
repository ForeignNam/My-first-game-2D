using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header("Game Over Screen")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private GameObject Instrcution;
    [SerializeField] private GameObject Instrcution1;
    [SerializeField] private GameObject Instrcution2;
    [SerializeField] private GameObject Instrcution3;


    [Header("Pause Game")]
    [SerializeField] private GameObject pauseScreen;
    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy)
            PauseGame(false);
            else
                PauseGame(true);
        }
    }
    #region Game Over Screen
    public void GameOver()
    {
               gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        SoundManager.Instance.PlaySound(gameOverSound);
        
    }
    public void Instrctions()
    {
        Instrcution.SetActive(false);
        Time.timeScale = 1;
    }
    public void Instrctions1()
    {
        Instrcution1.SetActive(false);
        Time.timeScale = 1;
    }
    public void Instrctions2()
    {
        Instrcution2.SetActive(false);
        Time.timeScale = 1;
    }
    public void Instrctions3()
    {
        Instrcution3.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void Quit()
    {
       Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    #region Pause Menu
    public void PauseGame(bool status)
    {
        //if (status)== true pause| | status == false unpause
        pauseScreen.SetActive(status);
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

    }
    public void SoundVolume()
    {
       SoundManager.Instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume(0.2f);
    }
    #endregion
}
