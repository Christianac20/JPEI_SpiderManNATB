using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [Header("Pause Menu")]
    public GameObject pausePanel;

    [Header("Settings")]
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);

        if (SettingsManager.Instance != null)
        {
            fullscreenToggle.isOn = SettingsManager.Instance.GetFullscreen();
            volumeSlider.value = SettingsManager.Instance.GetVolume();
        }

        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
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