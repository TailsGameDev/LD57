using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public enum SoundId
    {
        NONE = 0,
        MUSIC = 1,
    }
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
    private AudioSource[] sfxAudioSources = null;

    [SerializeField]
    private SoundRegister[] sondRegisters = null;

    private static SoundsManager instance;

    private int sfxAudioSourceIndex;


    public const float DEFAULT_MUSIC_VOLUME = 0.5f;
    public const string PLAYERPREFS_MUSIC_VOLUME_TAG = "MUSIC_VOLUME";


    public static SoundsManager Instance { get => instance; }

    public void Initialize()
    {
        instance = this;

        SetMusicVolume(DEFAULT_MUSIC_VOLUME);
        Debug.LogError("play music");
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
        PlayerPrefs.SetFloat(PLAYERPREFS_MUSIC_VOLUME_TAG, value);
    }
    public void SetSFXVolume(float value)
    {
        for (int s = 0; s < sfxAudioSources.Length; s++)
        {
            sfxAudioSources[s].volume = value;
        }
    }
}