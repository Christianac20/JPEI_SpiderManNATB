using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitHealPlayer : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int healthToHeal;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth.currentHealth += healthToHeal;
            Destroy(this.gameObject);
        }
    }
}
