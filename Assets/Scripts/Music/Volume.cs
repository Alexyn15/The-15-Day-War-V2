using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public AudioSource audioSource;   
    public Slider volumeSlider;      

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);

        audioSource.volume = savedVolume;
        volumeSlider.value = savedVolume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("GameVolume", volume);
    }
}
