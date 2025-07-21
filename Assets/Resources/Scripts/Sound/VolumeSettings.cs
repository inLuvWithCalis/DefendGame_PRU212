using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    //set volume ban đầu khi start

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMucsicVolume();
            SetSFXVolume();
        }
    }
    public void SetMucsicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        // myMixer.SetFloat("music", Mathf.Lerp(-80f, 0f, volume));
        PlayerPrefs.SetFloat("musicVolume", volume);
        Debug.LogError("Music volume set to: " + volume);
    }
    public void SetSFXVolume()
    {
        float volumesfx = SFXSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volumesfx) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volumesfx);

    }
    private void LoadVolume()
    {
        Debug.Log(musicSlider.value);
        Debug.Log(PlayerPrefs.GetFloat("sfx"));
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMucsicVolume();
        SetSFXVolume();
    }
}
