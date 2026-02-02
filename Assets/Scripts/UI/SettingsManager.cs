using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [Header("Referencias")]
    public AudioMixer audioMixer;

    // Variables de configuración
    private bool isFullscreen;
    private float volume;

    void Awake()
    {
        // Implementación del Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings(); // Carga configuración guardada
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Método para cambiar pantalla completa
    public void SetFullscreen(bool fullscreen)
    {
        isFullscreen = fullscreen;
        Screen.fullScreen = fullscreen;
        SaveSettings();
    }

    // Método para cambiar volumen
    public void SetVolume(float vol)
    {
        volume = vol;
        AudioListener.volume = vol;

        // Si usas AudioMixer, descomentar:
        // if (audioMixer != null)
        // {
        //     audioMixer.SetFloat("Volumen", Mathf.Log10(vol) * 20);
        // }

        SaveSettings();
    }

    // Obtener valores actuales
    public bool GetFullscreen() => isFullscreen;
    public float GetVolume() => volume;

    // Guardar configuración con PlayerPrefs
    void SaveSettings()
    {
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    // Cargar configuración
    void LoadSettings()
    {
        isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        volume = PlayerPrefs.GetFloat("Volume", 1f);

        // Aplicar configuración cargada
        Screen.fullScreen = isFullscreen;
        AudioListener.volume = volume;

        Debug.Log($"Ajustes cargados - Pantalla completa: {isFullscreen}, Volumen: {volume}");
    }
}