using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider masterSlider, musicSlider, soundSlider;

    void OnEnable()
    {
        masterSlider.value = AudioManager.Instance.MasterFloat;
        musicSlider.value = AudioManager.Instance.MusicFloat;
        soundSlider.value = AudioManager.Instance.SoundFloat;
    }

    public void SliderMasterVolumeChanged()
    {
        AudioManager.Instance.MasterVolume = masterSlider.value;
    }
    public void SliderMusicVolumeChanged()
    {
        AudioManager.Instance.MusicVolume = musicSlider.value;
    }
    public void SliderSoundVolumeChanged()
    {
        AudioManager.Instance.SoundVolume = soundSlider.value;
    }
}
