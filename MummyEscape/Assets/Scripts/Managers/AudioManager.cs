using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip buttonClickSfx;
    public AudioClip damageSfx;
    public AudioClip failedSfx;
    public AudioClip wonSfx;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void FadeOutMusic(float fadeTime = 1f)
    {
        StartCoroutine(FadeOutMusicRoutine(fadeTime));
    }

    IEnumerator FadeOutMusicRoutine(float fadeTime)
    {
        float initialVolume = musicSource.volume;

        while (musicSource.volume >= 0)
        {
            musicSource.volume -= initialVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = 1f;
    }

    public void FadeInMusic(float fadeTime = 1f)
    {
        StartCoroutine(FadeInMusicRoutine(fadeTime));
    }

    IEnumerator FadeInMusicRoutine(float fadeTime)
    {
        float initialVolume = 0.1f;

        while (musicSource.volume <= 1)
        {
            musicSource.volume += initialVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        musicSource.volume = 0f;
    }

    //Play sound with custom volume level
    public void PlaySound(AudioClip clip, float vol = 1)
    {
        sfxSource.PlayOneShot(clip, vol);
    }

    //Play sound at position
    public void PlaySoundAtPosition(AudioClip clip, Vector3 pos, float vol = 1)
    {
        sfxSource.transform.position = pos;
        PlaySound(clip, vol);
    }
}
