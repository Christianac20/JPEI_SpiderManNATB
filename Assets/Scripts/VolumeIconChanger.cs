using UnityEngine;
using UnityEngine.UI;

public class VolumeIconChanger : MonoBehaviour
{
    [Header("Referencias UI")]
    public Slider volumeSlider;
    public Image iconImage;

    [Header("Sprites de Volumen")]
    public Sprite muteSprite;    // 0
    public Sprite lowSprite;     // 1% - 33%
    public Sprite mediumSprite;  // 34% - 66%
    public Sprite highSprite;    // 67% - 100%

    void Start()
    {
        // Suscribirse al evento del slider por código es más limpio
        volumeSlider.onValueChanged.AddListener(UpdateIcon);

        // Inicializar el icono al arrancar
        UpdateIcon(volumeSlider.value);
    }

    public void UpdateIcon(float value)
    {
        if (value <= -79)
        {
            iconImage.sprite = muteSprite;
        }
        else if (value < -40f)
        {
            iconImage.sprite = lowSprite;
        }
        else if (value < -15f)
        {
            iconImage.sprite = mediumSprite;
        }
        else
        {
            iconImage.sprite = highSprite;
        }
    }
}