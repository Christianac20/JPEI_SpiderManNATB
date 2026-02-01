using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image healthBarImage;

    [Header("Sprites de Vida (de 0 a vida máxima)")]
    [SerializeField] private Sprite[] healthSprites; 

    void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged.AddListener(UpdateHealthBar);
            UpdateHealthBar(playerHealth.GetCurrentHealth());
        }
        else
        {
            Debug.LogError("PlayerHealth no asignado en HealthBarUI");
        }
    }

    void UpdateHealthBar(int currentHealth)
    {
        if (currentHealth >= 0 && currentHealth < healthSprites.Length)
        {
            healthBarImage.sprite = healthSprites[currentHealth];
        }
        else
        {
            Debug.LogWarning($"Índice de vida fuera de rango: {currentHealth}");
        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged.RemoveListener(UpdateHealthBar);
        }
    }
}