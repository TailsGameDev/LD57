using UnityEngine;
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
    }

    public void OnMusicSlideValueChange()
    {
        SoundsManager.Instance.SetMusicVolume(musicSlider.value);
    }
    public void OnSFXSliderValueChange()
    {
        SoundsManager.Instance.SetSFXVolume(musicSlider.value);
    }
}
