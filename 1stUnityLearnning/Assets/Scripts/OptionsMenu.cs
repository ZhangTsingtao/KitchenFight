using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer theMixer;

    public TMP_Text masterLabel, musicLabel, sfxLabel;
    public Slider masterSlider, musicSlider, sfxSlider;


    private void Start()
    {
        theMixer.GetFloat("MasterVol", out float vol);
        masterSlider.value = vol;

        theMixer.GetFloat("MusicVol", out  vol);
        musicSlider.value = vol;

        theMixer.GetFloat("SFXVol", out  vol);
        sfxSlider.value = vol;

        masterLabel.text = (masterSlider.value + 80).ToString();
        musicLabel.text = (musicSlider.value + 80).ToString();
        sfxLabel.text = (sfxSlider.value + 80).ToString();
    }

    public void SetMasterVol()
    {
        masterLabel.text = (masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }
    public void SetMusicVol()
    {
        musicLabel.text = (musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }
    public void SetSFXVol()
    {
        sfxLabel.text = (sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }
}
