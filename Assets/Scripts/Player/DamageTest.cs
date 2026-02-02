using UnityEngine;

public class DamageTest : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void Start()
    {
        // Buscar el PlayerHealth en el mismo GameObject
        playerHealth = GetComponent<PlayerHealth>();

        // Si no está en el mismo GameObject, buscarlo en la escena
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }

        // Verificar si se encontró
        if (playerHealth == null)
        {
            Debug.LogError("No se pudo encontrar PlayerHealth en la escena");
        }
        else
        {
            Debug.Log("PlayerHealth encontrado correctamente");
        }
    }

    void Update()
    {
        if (playerHealth == null)
        {
            return;
        }

        // Presiona H para recibir daño
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Presionaste H - Aplicando daño");
            playerHealth.TakeDamage(1);
        }

        // Presiona J para curarse
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Presionaste J - Curando");
            playerHealth.Heal(1);
        }
    }
}