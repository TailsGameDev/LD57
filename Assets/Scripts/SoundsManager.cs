using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class SoundsManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicAudioSource = null;

    [SerializeField]
    private AudioSource[] sfxAudioSources = null;

    public static SoundsManager Instance;

    private int sfxAudioSourceIndex;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void PlayMusic()
    {
        musicAudioSource.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxAudioSources[sfxAudioSourceIndex].PlayOneShot(sfx);
        sfxAudioSourceIndex = (sfxAudioSourceIndex + 1) % sfxAudioSources.Length;
    }

    public void SetMusicVolume(float value)
    {
        musicAudioSource.volume = value;
    }
    public void SetSFXVolume(float value)
    {
        for (int s = 0; s < sfxAudioSources.Length; s++)
        {
            sfxAudioSources[s].volume = value;
        }
    }
}