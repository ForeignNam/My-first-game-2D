using UnityEngine;
using UnityEngine.UI;
public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changesound;
    [SerializeField] private AudioClip interactSound;
   private RectTransform rectTransform;
    private int currentposition;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        // Change position of the selection arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
         if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }
        // Interact with the selected option
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
        {
            Interact(); 
        }
    }
    private void ChangePosition(int _change)
    {
        currentposition += _change;
        if(_change != 0)
        {
            SoundManager.Instance.PlaySound(changesound);
        }
        if (currentposition < 0)
        {
            currentposition = options.Length - 1;
        }
        else if(currentposition > options.Length - 1)
        {
            currentposition = 0;
        }
        rectTransform.position = new Vector3(rectTransform.position.x, options[currentposition].position.y, rectTransform.position.z);
    }
    private void Interact()
    {
        SoundManager.Instance.PlaySound(interactSound);
        options[currentposition].GetComponent<Button>().onClick.Invoke();
       
    }
}
