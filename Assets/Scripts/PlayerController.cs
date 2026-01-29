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
    public float xSpeed = 3;
    float horizontalInput;
    float xSpeedMultiplier;
    public float jumpForce = 3;
    public float rigidbodySpeed;

    // Variables Animator
    [Header("Variables Animator")]
    bool isAttacking;
    bool isGrounded;
    bool isRunning;
    bool isJumping;
    bool jPress;
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
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        #region MOVEMENT

        //Miramos si está atacando
        AnimationTagCheck();

        //Corregimos la orientación del sprite
        FlipPlayer();

        // Is Jumping?
        if (Input.GetButtonDown("Jump") && isGrounded == true && isAttacking == false)
        {
            rigidbodyPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("IsGrounded", false);
        }

        // MOVIMIENTO
        if (isAttacking == false)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal"); //Detecta cuando pulsas las flechas Izquierda / Derecha
            transform.Translate(Vector2.right * Time.deltaTime * xSpeed * xSpeedMultiplier * horizontalInput);
            movement = new Vector2(horizontalInput, 0f);
        }

        #region SPRINTING
        //Nos aseguramos de que este pulsando o no el botón de correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            xSpeedMultiplier = 1.5f;
            isRunning = true;
        }
        else
        {
            xSpeedMultiplier = 1.0f;
            isRunning = false;
        }
        #endregion

        #endregion

        #region ANIMATOR VARIABLES SET
        animator.SetBool("Idle", movement == Vector2.zero);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsRunning", isRunning);
        //animator.SetBool("Jump", isJumping);
        //animator.SetTrigger("Attack");
        //animator.SetBool("Charge", isCharge);
        //animator.SetBool("PressKeyJ", jPress);
        //animator.SetBool("HoldKeyJ", jHold);
        #endregion 
    }

    #region METODOS GENERALES
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