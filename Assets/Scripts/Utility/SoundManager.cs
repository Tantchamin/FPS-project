using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioItem
{
    public string key = "";
    public AudioClip sound;
}


public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager GetInstance()
    {
        return SoundManager.instance;
    }

    public AudioSource bgmSource;
    public List<AudioSource> sfxSources = new List<AudioSource>();
    public List<AudioItem> sounds = new List<AudioItem>();

    private Dictionary<string, AudioClip> idToSfxClip = new Dictionary<string, AudioClip>();
    // private AudioClip defaultBgm;

    // private bool bgmIsMute = false;
    // private bool sfxIsMute = false;

    private void Awake()
    {
        Init();
        RestoreSound();
    }

    void Init()
    {
        if (!SoundManager.instance)
        {
            DontDestroyOnLoad(gameObject);
            SoundManager.instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void RestoreSound()
    {
        sounds = sounds.FindAll(s => !string.IsNullOrEmpty(s.key));
        foreach (var s in sounds)
        {
            idToSfxClip[s.key] = s.sound;
        }
    }

    public void PlaySound(string soundName, bool isLoop, AudioSource audioSource = null, float volume = 0f)
    {
        if (!idToSfxClip.ContainsKey(soundName))
        {
            Debug.Log(soundName);
            Debug.LogWarning("Audio not found");
            return;
        }

        var clip = idToSfxClip[soundName];
        AudioSource playSource = audioSource == null ? sfxSources[0] : audioSource;

        const float defaultVolume = 1;
        float soundVolume = volume == 0 ? defaultVolume : volume;

        if(isLoop)
        {
            playSource.clip = clip;
            playSource.loop = true;
            playSource.volume = soundVolume;
            playSource.Play();
        }
        else
        {
            playSource.loop = false;
            playSource.volume = soundVolume;
            playSource.PlayOneShot(clip);
        }
    }

    public void PlayBgm(string soundName, bool isLoop)
    {
        bgmSource.loop = isLoop;
        bgmSource.clip = idToSfxClip[soundName];
        bgmSource.Play();
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }

    public void PlayButton(string soundName)
    {
        var clip = idToSfxClip[soundName];
        sfxSources[0].volume = 1;
        sfxSources[0].PlayOneShot(clip);
    }

}
