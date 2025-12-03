using UnityEngine;
using UISwitcher;

public class MusicToggle : MonoBehaviour
{
    public UISwitcher.UISwitcher switcher;  
    public AudioSource bgm;                

    void Start()
    {

        switcher.SetValue(bgm.isPlaying);


        switcher.onValueChanged.AddListener(OnMusicToggle);
    }

    
    public void OnMusicToggle(bool isOn)
    {
        if (isOn) bgm.Play();
        else bgm.Pause();
    }
}
