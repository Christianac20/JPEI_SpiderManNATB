using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    [SerializeField] private int maxHealth = 6;
    private int currentHealth;

    [Header("Eventos")]
    public UnityEvent<int> OnHealthChanged;
    public UnityEvent OnPlayerDeath;

    void Awake() // Cambiar de Start a Awake
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        // Invocar el evento después de inicializar
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        // Validación extra
        if (damage < 0)
        {
            Debug.LogWarning("El daño no puede ser negativo");
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Jugador recibió {damage} de daño. Vida actual: {currentHealth}");

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("La curación no puede ser negativa");
            return;
        }

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Jugador curado {amount}. Vida actual: {currentHealth}");

        OnHealthChanged?.Invoke(currentHealth);
    }

    void Die()
    {
        Debug.Log("El jugador ha muerto");
        OnPlayerDeath?.Invoke();
        // Aquí puedes agregar lógica de muerte
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}