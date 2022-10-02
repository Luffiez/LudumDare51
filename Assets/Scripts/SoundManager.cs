using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioClip startClip;

    [SerializeField] List<AudioClip> soundEffects;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        DontDestroyOnLoad(gameObject);
        if(startClip)
            PlayBGM(startClip);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }

    void PlaySfx(AudioClip clip, float volume = 1)
    {
        sfx.PlayOneShot(clip, volume);
    }

    public void PlaySfx(string name, float volume = 1)
    {
        var effect = soundEffects.Find(c => c.name == name);
        if (!effect)
            throw new IndexOutOfRangeException($"No AudioClip with name '{name}' in soundEffects List!");

        PlaySfx(effect, volume);
    }
}
