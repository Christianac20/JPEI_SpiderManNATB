using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damageDone = 1;
    public Animator animator;
    public bool isAttacking;
    bool isGrounded;
    EnemyHealth enemyHealth;

    public GameObject webShootProjectile; //almacena el proyectil
    public WebShooter webShooter;

    //Manejo de audio
    public AudioManager audioManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        #region ATAQUES
        // Llamo a las funciones de ataque
        Punch();
        Kick();
        MegaPunch();
        ShootWeb();
        #endregion

        // Animator
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking == true || collision.CompareTag("Enemy") || collision.CompareTag("Destructible"))
        {
            //collision.SendMessageUpwards("AddDamage");
            collision.SendMessageUpwards("TakeDamage"); 
        }
    }

    #region METODOS DE ATAQUES
    // Ataque del puño purificador. Cambio en las tecas respecto al plan iniciar de mantener las del Punch
    private void MegaPunch()
    {
        // Con el input manager para atacar MegaPunch
        if (isGrounded && !isAttacking && Input.GetButtonDown("Mega Punch")) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            damageDone = 5;
            animator.SetTrigger("AttackMegaPunch"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
            audioManager.PlaySFX(audioManager.Purificator_01);
        }
    }

    // Ataque basico de puñetazo
    private void Punch()
    {
        // Con el input manager para atacar Punch
        if (isGrounded && !isAttacking && isAttacking == false && Input.GetButtonDown("Punch")) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            damageDone = 2; 
            animator.SetTrigger("AttackPunch"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
            audioManager.PlaySFX(audioManager.Punch_01);
        }
    }

    // Ataque basico de patada
    private void Kick()
    {
        // Con el input manager para atacar Kick
        if (isGrounded && !isAttacking && Input.GetButtonDown("Kick")) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            damageDone = 2;
            animator.SetTrigger("AttackKick"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
            audioManager.PlaySFX(audioManager.Kick_01);
        }
    }

    // Ataque basico de disparo de telaraña
    private void ShootWeb()
    {
        // Con el input manager para atacar ShootWeb
        if (isGrounded && !isAttacking && Input.GetButtonDown("Shoot Web")) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            damageDone = 1;
            animator.SetTrigger("AttackShootWeb"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
            audioManager.PlaySFX(audioManager.WebShoot_01);
            webShooter.Shoot();
        }
    }
    #endregion

    #region ISGROUNDED CHECKING
    void OnCollisionEnter2D(Collision2D collision)
    {
        //movementScript.isGrounded = true;
        if (collision.gameObject.tag == ("Ground") || collision.gameObject.tag == ("Destructible"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground") || collision.gameObject.tag == ("Destructible"))
        {
            isGrounded = false;
        }
    }
    #endregion
}
