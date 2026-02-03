using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    [SerializeField] private int maxHealth = 6;
    public int currentHealth;

    [Header("Eventos")]
    public UnityEvent<int> OnHealthChanged;
    public UnityEvent OnPlayerDeath;
    public PlayerAttack playerAttack;
    public PlayerController playerController;
    public HealthBarUI healthBarUI;
    public Animator animator;
    public GameObject deathCanvas;

    void Awake() // Cambiar de Start a Awake
    {
        currentHealth = maxHealth;
        playerAttack = GetComponent<PlayerAttack>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Invocar el evento después de inicializar
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
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
        playerController.enabled = false;
        playerAttack.enabled = false;
        animator.SetTrigger("Death");
        deathCanvas.SetActive(true);
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}