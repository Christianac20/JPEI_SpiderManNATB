// EN LA FOLDER DEL PLAYER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    [SerializeField] private int maxHealth = 6;
    private int currentHealth;
    public int totalHealth = 3;
    //public RectTransform heartUI; // Porque ana tenia el elemento de canvas del heart

    //Game Over
    //public RectTransform gameOverMenu; //Menu de game Over
    //public GameObject hordes; // Porque ana lo tenia con hordas

    [Header("Eventos")]
    public UnityEvent<int> OnHealthChanged; // Evento para actualizar UI
    public UnityEvent OnPlayerDeath;

    private int health;
    //private float heartSize = 16f;

    private SpriteRenderer playerRenderer;
    private Animator animator;
    private PlayerController controller;
    private Vector2 startPosition;


    private void Awake()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        startPosition = transform.position;
    }

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    /*public void AddDamage(int amount)
    {
        health = health - amount;

        // Visual Feedback
        StartCoroutine("VisualFeedback");

        // Game  Over
        if (health <= 0)
        {
            health = 0;
            OnDisable();
        }

        //heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);

        Debug.Log("Player got damaged. His current health is " + health);
    }*/

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void AddHealth(int amount)
    {
        health = health + amount;

        // Max health
        if (health > totalHealth)
        {
            health = totalHealth;
        }

        //heartUI.sizeDelta = new Vector2(heartSize * health, heartSize); //Porque ana lo hizo con aquello de tilear corazones
        
        Debug.Log("Player got some life. His current health is " + health);
    }

    private IEnumerator VisualFeedback()
    {
        playerRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        playerRenderer.color = Color.white;
    }

    public void OnEnable()
    {
        health = totalHealth;
        //heartUI.sizeDelta = new Vector2(heartSize * health, heartSize); //Porque ana lo hizo con aquello de tilear corazones
        transform.position = new Vector2(startPosition.x, startPosition.y);
        gameObject.SetActive(true);

    }

    private void OnDisable()
    {
        /*
        if (gameOverMenu != null)
            gameOverMenu.gameObject.SetActive(true);

        if (hordes != null)
            hordes.SetActive(false);
        //Destroy(hordes);
        */

        if (animator != null)
            animator.enabled = false;

        if (controller != null)
            controller.enabled = false;
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
        Debug.Log("El jugador ha muerto");
    }

    // Getters
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}
