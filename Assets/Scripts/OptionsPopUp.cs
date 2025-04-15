using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsPopUp : MonoBehaviour
{
    [SerializeField]
    private Slider musicSlider = null;
    [SerializeField]
    private Slider sfxSlider = null;

    public void ShowPopUp(bool show)
    {
        gameObject.SetActive(show);

        musicSlider.value = PlayerPrefs.GetFloat(SoundsManager.PLAYERPREFS_MUSIC_VOLUME_TAG, defaultValue: SoundsManager.DEFAULT_MUSIC_VOLUME);
    }

    public void OnMusicSlideValueChange()
    {
        SoundsManager.Instance.SetMusicVolume(musicSlider.value);
    }
    public void OnSFXSliderValueChange()
    {
        SoundsManager.Instance.SetSFXVolume(musicSlider.value);
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
