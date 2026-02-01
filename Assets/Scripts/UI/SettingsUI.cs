using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    void OnEnable() // Cambiar de Start a OnEnable para que se actualice cada vez que se active
    {
        LoadCurrentSettings();

        // Agregar listeners
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnDisable()
    {
        // Remover listeners para evitar duplicados
        fullscreenToggle.onValueChanged.RemoveListener(OnFullscreenChanged);
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
    }

    void LoadCurrentSettings()
    {
        if (SettingsManager.Instance != null)
        {
            // Cargar sin disparar eventos
            fullscreenToggle.SetIsOnWithoutNotify(SettingsManager.Instance.GetFullscreen());
            volumeSlider.SetValueWithoutNotify(SettingsManager.Instance.GetVolume());
        }
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