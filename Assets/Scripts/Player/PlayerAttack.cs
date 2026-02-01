using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageDone;
    public Animator animator;
    bool isAttacking;
    bool isGrounded;
    EnemyHealth enemyHealth;

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
        if (isAttacking == true)
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Box"))
            {
                //collision.SendMessageUpwards("AddDamage");
                collision.SendMessageUpwards("TakeDamage");
            }    
        }
    }

    #region METODOS DE ATAQUES
    // Ataque del puño purificador. Cambio en las tecas respecto al plan iniciar de mantener las del Punch
    private void MegaPunch()
    {
        // Con el input manager para atacar MegaPunch
        if (isGrounded && Input.GetButtonDown("Mega Punch")) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            damageDone = 5/2; //Por ahora asi para que en verdad haga el daño grande. Por algun bug es como que llama a la funcion 2 veces y hace daño doble???
            animator.SetTrigger("AttackMegaPunch"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
        }
    }

    // Ataque basico de puñetazo
    private void Punch()
    {
        // Con el input manager para atacar Punch
        if (isGrounded && isAttacking == false && Input.GetButtonDown("Punch")) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            damageDone = 2/2; //Por ahora asi para que en verdad haga el daño grande. Por algun bug es como que llama a la funcion 2 veces y hace daño doble???
            animator.SetTrigger("AttackPunch"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
        }
    }

    // Ataque basico de patada
    private void Kick()
    {
        // Con el input manager para atacar Kick
        if (isGrounded && Input.GetButtonDown("Kick")) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            damageDone = 2/2; //Por ahora asi para que en verdad haga el daño grande. Por algun bug es como que llama a la funcion 2 veces y hace daño doble???
            animator.SetTrigger("AttackKick"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
        }
    }

    // Ataque basico de disparo de telaraña
    private void ShootWeb()
    {
        // Con el input manager para atacar ShootWeb
        if (isGrounded && Input.GetButtonDown("Shoot Web")) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            damageDone = 1/2; //Por ahora asi para que en verdad haga el daño grande. Por algun bug es como que llama a la funcion 2 veces y hace daño doble???
            animator.SetTrigger("AttackShootWeb"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
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
