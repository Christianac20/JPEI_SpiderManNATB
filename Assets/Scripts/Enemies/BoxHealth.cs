using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    public float maxHealth = 10.0f;
    public float currentHealth;
    public PlayerAttack playerAttack;
    public Animator animator;
    public float doThisDamage;
    public GameObject medkitPrefab;
    public Transform spawnLocation;

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
        StartCoroutine("TakeDamageCorroutine");
    }

    IEnumerator TakeDamageCorroutine()
    {
        currentHealth -= playerAttack.damageDone;

        if (currentHealth <= 0.0f)
        {
            animator.SetTrigger("Death");
            yield return new WaitForSeconds(1.0f);
            GameObject myMedkit = Instantiate(medkitPrefab,spawnLocation.position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}