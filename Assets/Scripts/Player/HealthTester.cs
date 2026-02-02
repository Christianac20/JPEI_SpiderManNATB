using UnityEngine;

public class HealthTester : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private int healAmount = 1;

    void Update()
    {
        // Presiona Q para recibir daño
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Daño aplicado con Q");
        }

        // Presiona E para curarse
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerHealth.Heal(healAmount);
            Debug.Log("Curación aplicada con E");
        }

        // Presiona R para resetear vida al máximo
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerHealth.Heal(playerHealth.GetMaxHealth());
            Debug.Log("Vida reseteada");
        }

        // Presiona T para reducir vida a la mitad
        if (Input.GetKeyDown(KeyCode.T))
        {
            int halfDamage = playerHealth.GetCurrentHealth() / 2;
            playerHealth.TakeDamage(halfDamage);
            Debug.Log("Vida reducida a la mitad");
        }
    }
}