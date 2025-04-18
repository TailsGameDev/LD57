using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundsManager : MonoBehaviour
{



    [System.Serializable]
    private struct SoundRegister
    {
        [SerializeField]
        private SoundId id;
        [SerializeField]
        private AudioClip audioClip;

        public SoundId Id { get => id; }
        public AudioClip AudioClip { get => audioClip; }
    }


    [SerializeField]
    private AudioSource musicAudioSource = null;

    [SerializeField]
    private AudioSource[] reservedAudioSources = null;


    [SerializeField]
    [FormerlySerializedAs("sfxAudioSources")]
    private AudioSource[] genericAudioSources = null;

    [SerializeField]
    private List<SoundRegister> soundRegisters = null;

    private static SoundsManager instance;

    private int sfxAudioSourceIndex;


    public const float DEFAULT_MUSIC_VOLUME = 0.5f;
    public const string PLAYERPREFS_MUSIC_VOLUME_TAG = "MUSIC_VOLUME";


    public static SoundsManager Instance { get => instance; }

    public void Initialize()
    {
        instance = this;

        SetMusicVolume(DEFAULT_MUSIC_VOLUME);
        musicAudioSource.Play();
    }

    public void SetMusicVolume(float value)
    {
        musicAudioSource.volume = value;
        PlayerPrefs.SetFloat(PLAYERPREFS_MUSIC_VOLUME_TAG, value);
    }
    public void SetSFXVolume(float value)
    {
        for (int s = 0; s < genericAudioSources.Length; s++)
        {
            genericAudioSources[s].volume = value;
        }
    }

    public void PlayReserved(SoundId sound, SoundLayer layer)
    {
        AudioClip clip = soundRegisters.Find(register => register.Id.Equals(sound)).AudioClip;
        reservedAudioSources[(int)layer].PlayOneShot(clip);
    }

    public void PlayReservedPolitely(SoundId sound, SoundLayer layer)
    {
        int index = (int)layer;

        if (!reservedAudioSources[index].isPlaying)
        {
            AudioClip clip = soundRegisters.Find(register => register.Id.Equals(sound)).AudioClip;
            reservedAudioSources[index].PlayOneShot(clip);
        }
    }

    public void StopReserved(SoundLayer layer)
    {
        reservedAudioSources[(int)layer].Stop();
    }

    public void PlayGeneric(SoundId sound)
    {
        AudioClip clip = soundRegisters.Find(register => register.Id.Equals(sound)).AudioClip;
        genericAudioSources[sfxAudioSourceIndex].PlayOneShot(clip);
        sfxAudioSourceIndex = (sfxAudioSourceIndex + 1) % genericAudioSources.Length;
    }
}
public enum SoundId
{
    NONE = 0,
    MUSIC = 1,
    LINGUA_INDO = 2,
    LINGUA_VOLTANDO = 3
}

public enum SoundLayer
{
    MUSIC = 0,
    LINGUA = 1,
    FORMIGA = 2,
    VOLTANDO = 3
}