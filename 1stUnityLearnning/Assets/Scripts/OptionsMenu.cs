using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer theMixer;

    public TMP_Text masterLabel, musicLabel, sfxLabel, mouseSenLabel;
    public Slider masterSlider, musicSlider, sfxSlider, mouseSenSlider;

    public CinemachineFreeLook freelookCam;

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

        mouseSenSlider.value = PlayerPrefs.GetFloat("MouseSen");
        mouseSenLabel.text = mouseSenSlider.value.ToString();        
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

    public void SetMouseSen()
    {
        mouseSenLabel.text = (mouseSenSlider.value).ToString();

        PlayerPrefs.SetFloat("MouseSen", mouseSenSlider.value);

        freelookCam.m_XAxis.m_MaxSpeed = PlayerPrefs.GetFloat("MouseSen") * 2 + 100f;
        freelookCam.m_YAxis.m_MaxSpeed = PlayerPrefs.GetFloat("MouseSen") / 50f + 1f;
    }
}
