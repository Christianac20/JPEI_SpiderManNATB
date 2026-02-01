using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void PantallaCompleta(bool pantallaCompleta)
    {
        // Usar el SettingsManager en lugar de hacerlo directamente
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.SetFullscreen(pantallaCompleta);
        }
        else
        {
            Screen.fullScreen = pantallaCompleta;
        }
    }

    public void CambiarVolumen(float volumen) // Corregí el nombre del método
    {
        // Usar el SettingsManager
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.SetVolume(volumen);
        }

        // Si quieres usar AudioMixer también:
        if (audioMixer != null)
        {
            // Convertir volumen lineal (0-1) a decibeles
            float volumeDB = volumen > 0 ? Mathf.Log10(volumen) * 20 : -80f;
            audioMixer.SetFloat("Volumen", volumeDB);
        }
    }
}