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
    #endregion VARIABLES

    void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        #region MOVEMENT
        //Actualizamos la velocidad del Rigidbody cada frame
        rigidbodySpeed = rigidbodyPlayer.velocity.magnitude;

        if (isAttacking == false)
        {
            // Movement
            float horizontalInput = Input.GetAxisRaw("Horizontal"); //Guardamos en la variable horizontalInput si el user ha pulsado la flecha izda o dcha o mueve el joystick
            movement = new Vector2(horizontalInput, 0f);

            FlipPlayer();//Corregimos la orientación del sprite
        }

        // Is Jumping?
        if (Input.GetButtonDown("Jump") && isGrounded == true && isAttacking == false)
        {
            rigidbodyPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("IsGrounded", false);
        }
        #endregion

        #region MOVEMENT MODIFIERS
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

        //Miramos si está atacando
        AnimationTagCheck();

        //Aplicamos el movimiento
        #region MOVEMENT
        if (isAttacking == false)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal"); //Detecta cuando pulsas las flechas Izquierda / Derecha

            transform.Translate(Vector2.right * Time.deltaTime * xSpeed * xSpeedMultiplier * horizontalInput);
        }
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
}