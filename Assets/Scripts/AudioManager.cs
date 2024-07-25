using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicSource;
    public AudioSource RainSource;
    public AudioSource sfxSource;
    public List<AudioClip> backgroundSoundsclipList;
    public List<AudioClip> SfxSoundsclipList;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        musicSource = gameObject.AddComponent<AudioSource>();
        RainSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        sfxSource.playOnAwake = false;
        RainSource.playOnAwake = false;
        musicSource.loop = true;
        RainSource.loop = true;
        musicSource.volume = 1.0f;
        sfxSource.volume = 1.0f;
        RainSource.volume = 0.5f;
        PlayMusic(backgroundSoundsclipList[0]);
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void PlayRain(AudioClip clip)
    {
        RainSource.clip = clip;
        RainSource.Play();
    }
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        SetRainVolume(volume/2);
    }
    void SetRainVolume(float volume)
    {
        RainSource.volume = volume;
    }

}
