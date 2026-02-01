using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10.0f;
    public float currentHealth;
    public PlayerAttack playerAttack;

    /*
    public void AddDamage()
    {
        gameObject.SetActive(false);
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage()
    {
        Debug.Log("TakeDamage solicitado");
        
        currentHealth -= playerAttack.damageDone;

        if (currentHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}