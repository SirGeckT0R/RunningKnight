using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    private string sliderName;
    private Slider slider;
    private SoundManager manager;

    private void Awake()
    {
        slider= GetComponent<Slider>();
        manager= GameObject.Find("SoundManager").GetComponent<SoundManager>();
        sliderName = gameObject.name;
    }
    public void Start()
    {
        switch (sliderName)
        {
            case "VolumeSlider":
                slider.value = manager.GetSoundVolume();
                break;
            case "MusicSlider":
                slider.value = manager.GetMusicVolume();
                break;
            default:
                break;
        }
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    { 
        switch (sliderName)
        {
            case "VolumeSlider":
                manager.ChangeSoundVolume(slider.value);
                break;
            case "MusicSlider":
                manager.ChangeMusicVolume(slider.value);
                break;
            default:
                break;
        }
    }
}
