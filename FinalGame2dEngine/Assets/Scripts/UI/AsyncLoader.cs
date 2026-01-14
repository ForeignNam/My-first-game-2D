using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
public class AsyncLoader : MonoBehaviour
{
    [Header(" Loading Screen Elements")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI loadingtext;
    [Header(" Behavior")]
    [SerializeField] private float loadingspeed;
    [SerializeField] private float tipchangeInterval= 1.5f;
    [SerializeField] private string menuSceneNam = "Mainmenu";




    [Header(" Loading Messages")]
    [TextArea]
    [SerializeField] string[] tips = new string[] {
    " You are really beautiful that makes me feel jeulous.....",
        "Every time I see you , you are more beautiful.....",
        "Your smile is like the sunshine that brightens my day.....",
        "Your eyes sparkle like stars in the night sky.....",
        "Your laughter is contagious and fills my heart with joy....."
    };
    int lasttipindex = -1;
    private bool isloading ;

    private void Start()
    {
       Application.targetFrameRate = 60;
        progressBar.value = 0f;
        StartCoroutine(Fakeloading());
        StartCoroutine(CycleTips());
    }
   
    IEnumerator Fakeloading()
    {
        while (progressBar.value < 1f)
        {
            progressBar.value += loadingspeed * Time.deltaTime;
            yield return null;
        }
        isloading = true;
        SceneManager.LoadScene(menuSceneNam);
    }
    IEnumerator CycleTips()
    {
        SetRandomTip();
        while (progressBar.value < 1f)
        {
            yield return new WaitForSeconds(tipchangeInterval);
            SetRandomTip();
        }
       
    }

    private void SetRandomTip()
    {
        if(tips == null || tips.Length == 0)
        {
            loadingtext.text = "";
            return;
        }
        int index= Random.Range(0, tips.Length);
        if(index== lasttipindex && tips.Length >1)
        {
            index = (index + 1) % tips.Length;
        }
        lasttipindex = index;
        if(loadingtext != null)
        {
            loadingtext.text = tips[index];
        }
    }

}
