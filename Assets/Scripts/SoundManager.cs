using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource bgm;

    [SerializeField] AudioClip startClip;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        if(startClip)
            PlayBGM(startClip);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }

    public void PlaySfx(AudioClip clip, float volume = 1)
    {
        sfx.PlayOneShot(clip, volume);
    }
}
