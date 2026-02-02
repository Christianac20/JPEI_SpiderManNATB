using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("Referencia al jugador")]
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Sprites de la barra de vida")]
    [SerializeField] private Sprite[] healthBarSprites; // Array de 7 sprites (0% a 100%)

    [Header("Componente de imagen")]
    [SerializeField] private Image healthBarImage;

    void Start()
    {
        // Validaciones
        if (playerHealth == null)
        {
            Debug.LogError("No se asignó PlayerHealth en el Inspector");
            return;
        }

        if (healthBarImage == null)
        {
            Debug.LogError("No se asignó el componente Image");
            return;
        }

        if (healthBarSprites == null || healthBarSprites.Length != 7)
        {
            Debug.LogError("Debes asignar exactamente 7 sprites en el array");
            return;
        }

        // Suscribirse al evento de cambio de vida
        playerHealth.OnHealthChanged.AddListener(UpdateHealthBar);

        // Actualizar la barra inicialmente
        UpdateHealthBar(playerHealth.GetCurrentHealth());
    }

    void UpdateHealthBar(int currentHealth)
    {
        int maxHealth = playerHealth.GetMaxHealth();

        // Calcular el porcentaje de vida
        float healthPercentage = (float)currentHealth / maxHealth;

        // Convertir el porcentaje a índice del array (0-6)
        int spriteIndex = Mathf.RoundToInt(healthPercentage * 6);
        spriteIndex = Mathf.Clamp(spriteIndex, 0, 6);

        // Cambiar el sprite
        healthBarImage.sprite = healthBarSprites[spriteIndex];
    }

    void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged.RemoveListener(UpdateHealthBar);
        }
    }
}