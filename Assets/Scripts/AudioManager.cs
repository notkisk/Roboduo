using UnityEngine;
using System;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    public AudioMixer _mixer;
    public AudioMixerGroup sfxGroup;

    public const string MUSIC_KEY = "MusicVolume";
    public const string SFX_KEY = "SfxVolume";

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.outputAudioMixerGroup = sfxGroup;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
        LoadVolumes();
    }
    void LoadVolumes()
    {
        float SfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 0f);
        float MusicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0f);

        SetVolume(SFX_KEY, SfxVolume);
        SetVolume(MUSIC_KEY, MusicVolume);



    }
    public void Play(string name)
    {
       Sound s=  Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

    }
    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();

    }

    public void SetVolume(string name,float volume)
    {
        PlayerPrefs.SetFloat(name, volume);
        _mixer.SetFloat(name, volume);
    }


}
