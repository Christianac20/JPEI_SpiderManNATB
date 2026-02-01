using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    void Start()
    {
        // Cargar valores actuales del SettingsManager
        if (SettingsManager.Instance != null)
        {
            fullscreenToggle.isOn = SettingsManager.Instance.GetFullscreen();
            volumeSlider.value = SettingsManager.Instance.GetVolume();
        }

        // Agregar listeners
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnFullscreenChanged(bool value)
    {
        SettingsManager.Instance?.SetFullscreen(value);
    }

    void OnVolumeChanged(float value)
    {
        SettingsManager.Instance?.SetVolume(value);
    }
}