using UnityEngine;

public class DamageTest : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    void Update()
    {
        // Presiona la tecla H para recibir daño
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerHealth.TakeDamage(1);
        }

        // Presiona la tecla J para curarse
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerHealth.Heal(1);
        }
    }
}