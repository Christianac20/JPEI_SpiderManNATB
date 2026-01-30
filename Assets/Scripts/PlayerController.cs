using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES
    // Variables Float
    [Header("Variables Float")]
    float horizontalInput;
    public float xSpeedMultiplier = 1.0f;
    public float xSpeed = 3;
    public float jumpForce = 3;

    // Variables Animator
    [Header("Variables del Animator")]
    bool isAttacking;
    bool isGrounded;
    bool isRunning;
    bool isJumping;
    //bool jHold;

    //Variables de Componente
    [Header("Variables de Componente")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rigidbodyPlayer;

    //Variables Compuestas
    [Header("Variables compuestas")]
    public Vector2 movement;
    #endregion

    void Awake() //Lo uso para guardar componentes al iniciar
    {
        #region GUARDAR REFERENCIAS
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        #endregion
    }

    void Update()
    {
        #region MOVEMENT
        // Movimiento lateral basico
        if (isAttacking == false)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal"); //Detecta cuando pulsas las flechas Izquierda / Derecha
            transform.Translate(Vector2.right * Time.deltaTime * xSpeed * xSpeedMultiplier * horizontalInput);
            movement = new Vector2(horizontalInput, 0f);
        }

        // Llamo a las funciones modificadoras del movimiento
        AnimationTagCheck();
        FlipPlayer();
        Jump();
        Run();
        #endregion

        #region ATAQUES
        // Llamo a las funciones de ataque
        Punch();
        Kick();
        MegaPunch();
        ShootWeb();
        #endregion

        #region ANIMATOR VARIABLES SET
        // Asigno variables a parametros del animator
        animator.SetBool("Idle", movement == Vector2.zero);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsRunning", isRunning);
        #endregion 
    }

    #region METODOS MODIFICADORES DEL MOVIMIENTO

    //Corregimos la orientación del player
    private void FlipPlayer()
    {
        if (horizontalInput > 0.01)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < -0.01)
        {
            spriteRenderer.flipX = true;
        }
    }

    // Is Jumping?
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true && isAttacking == false)
        {
            rigidbodyPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("IsGrounded", false);
        }
    }

    // Nos aseguramos de que este pulsando o no el botón de correr
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            xSpeedMultiplier = 2f;
            isRunning = true;
        }
        else
        {
            xSpeedMultiplier = 1.0f;
            isRunning = false;
        }
    }
    
    // Comprobacion de si está atacando
    private void AnimationTagCheck()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    #endregion

    #region METODOS DE ATAQUES
    // Ataque del puño purificador. Cambio en las tecas respecto al plan iniciar de mantener las del Punch
    private void MegaPunch()
    {
        if (isGrounded && (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.P))) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            animator.SetTrigger("AttackMegaPunch"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
        }
    }

    // Ataque basico de puñetazo
    private void Punch()
    {
        if (isGrounded && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z))) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            animator.SetTrigger("AttackPunch"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
        }
    }

    // Ataque basico de patada
    private void Kick()
    {
        if (isGrounded && (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.X))) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            animator.SetTrigger("AttackKick"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
        }
    }

    // Ataque basico de disparo de telaraña
    private void ShootWeb()
    {
        if (isGrounded && (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.C))) // SI esta en el suelo y se pulsan las teclas para ese ataque
        {
            animator.SetTrigger("AttackShootWeb"); // Activo el trigger correspondiente a este ataque para reproducir la animacion
        }
    }

    #endregion

    #region ISGROUNDED CHECKING
    void OnCollisionEnter2D(Collision2D collision)
    {
        //movementScript.isGrounded = true;
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = false;
        }
    }
    #endregion
    
}