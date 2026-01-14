using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource audioSource;
    private AudioSource musicSource;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        // Load saved volume settings or set defaults
        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, audioSource);
    }
    public void ChangeMusicVolume(float _change)
    {
         ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }
    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }
        float finalVolume = baseVolume * currentVolume;
        source.volume = finalVolume;
        PlayerPrefs.SetFloat(volumeName, currentVolume);



    }
}
