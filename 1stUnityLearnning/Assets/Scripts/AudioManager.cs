using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public AudioMixer theMixer;

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            switch (s.audioType)
            {
                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;

                case Sound.AudioTypes.sfx:
                    s.source.outputAudioMixerGroup = sfxMixerGroup;
                    break;
            }
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol"))
            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        
        if (PlayerPrefs.HasKey("MusicVol"))
            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        
        if (PlayerPrefs.HasKey("SFXVol"))
            theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));

        Play("theme");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (name == null)
        {
            Debug.LogWarning("Sound Clip: " + name + "not found");
            return;
        }
        s.source.Play();
    }
}
