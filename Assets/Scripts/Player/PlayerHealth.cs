using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    [SerializeField] private int maxHealth = 6;
    public int currentHealth;

    [Header("Eventos")]
    public UnityEvent<int> OnHealthChanged; // Evento para actualizar UI
    public UnityEvent OnPlayerDeath;
    public Animator animator;
    public AudioManager audioManager;
    public PlayerController playerController;
    public PlayerAttack playerAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        audioManager.PlaySFX(audioManager.Hurt_01);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para curar
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth);
    }

    void Die()
    {
        OnPlayerDeath?.Invoke();
        animator.SetTrigger("Death");
        Debug.Log("El jugador ha muerto");
        playerController.enabled = false;
        playerAttack.enabled = false;
    }

    // Getters
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}