using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
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

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}