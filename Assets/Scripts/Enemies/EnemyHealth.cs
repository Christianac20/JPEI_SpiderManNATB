using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10.0f;
    public float currentHealth;
    public PlayerAttack playerAttack;
    public Animator animator;
    public EnemyPatrol enemyPatrol;
    public float doThisDamage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        doThisDamage = playerAttack.damageDone;
    }

    public void TakeDamage()
    {
        StartCoroutine("TakeDamageCorroutine");
    }

    IEnumerator TakeDamageCorroutine()
    {
        Debug.Log("TakeDamage solicitado");

        currentHealth -= doThisDamage;
        Debug.Log("doThisDamage es " + doThisDamage);

        if (currentHealth <= 0.0f)
        {
            enemyPatrol.enabled = false;
            Debug.Log("enemyPatrol.enabled = false solicitado");
            animator.SetTrigger("Death");
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
    }
}