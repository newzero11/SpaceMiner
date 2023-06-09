using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class soundSlider : MonoBehaviour
{
    //To adjust the sound volume through the slider

    public AudioMixer audioMixer;
    public Slider musicSlider, sfxSlider;

    private void Start()
    {
        musicSlider.value = SoundManager.instance.saveMusicValue;
        sfxSlider.value = SoundManager.instance.saveSfxValue;
    }

    public void setMusicVolume() { //Set the music slider value to the volume of music
        audioMixer.SetFloat("Music", Mathf.Log10(musicSlider.value)*20);
        SoundManager.instance.saveMusicValue = musicSlider.value;
    }

    public void setSFXVolume() //Set the sound effect slider value to the volume of sfx
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
        SoundManager.instance.saveSfxValue = sfxSlider.value;
    }
}
