using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES
    // Variables Float
    [Header("Variables Float")]
    public float xSpeed = 3;
    float horizontalInput;
    float xSpeedMultiplier;
    public float jumpForce = 3;
    public float _rbSpeed;

    // Variables Animator
    [Header("Variables Animator")]
    bool isAttacking = false;
    bool isJumping;
    bool isGrounded;
    bool jPress;
    bool jHold;
    bool isCharge;

    //Variables de Componente
    [Header("Variables de Componente")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rigidbodyPlayer;

    //Variables del Jump
    [Header("Variables del Jump")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;

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
        if (isAttacking == false)
        {
            // Movement
            float horizontalInput = Input.GetAxisRaw("Horizontal"); //Guardamos en la variable horizontalInput si el user ha pulsado la flecha izda o dcha o mueve el joystick
            movement = new Vector2(horizontalInput, 0f);

            FlipPlayer();//Corregimos la orientación del sprite
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded == true && isAttacking == false)
        {
            rigidbodyPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("IsGrounded", false);
        }

        //Actualizamos la velocidad del Rigidbody cada frame
        _rbSpeed = rigidbodyPlayer.velocity.magnitude;

        /*
        //Corregimos la orientación del sprite
        FlipPlayer();
        */

        //Miramos si está atacando
        AnimationTagCheck();

        #region MOVEMENT MODIFIERS
        //Nos aseguramos de que este pulsando o no el botón de correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            xSpeedMultiplier = 1.5f;
        }
        else
        {
            xSpeedMultiplier = 1.0f;
        }

        //JAttack();
        Pinch();
        Jump();


        #endregion

        //Aplicamos el movimiento
        #region MOVEMENT
        if (isAttacking == false)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal"); //Detecta cuando pulsas las flechas Izquierda / Derecha

            transform.Translate(Vector2.right * Time.deltaTime * xSpeed * xSpeedMultiplier * horizontalInput);
        }
        #endregion

        #region AnimatorBools
        animator.SetBool("Idle", movement == Vector2.zero);
        //animator.SetBool("Jump", isJumping);
        animator.SetTrigger("Attack");
        //animator.SetBool("Charge", isCharge);
        animator.SetBool("PressKeyJ", jPress);
        animator.SetBool("HoldKeyJ", jHold);
        animator.SetFloat("PlayerSpeedX", xSpeed);
        #endregion 
    }

    void FixedUpdate() //Metodo para hacer modificaciones en el rigid body del player
    {
        if (isAttacking == false)
        {
            float horizontalVelocity = movement.normalized.x * xSpeed * xSpeedMultiplier;
            rigidbodyPlayer.velocity = new Vector2(horizontalVelocity, rigidbodyPlayer.velocity.y);
        }
    }

    void LateUpdate() //Metodo para hacer modificaciones en el animator de objetos fisicos dinamicos
    {

    }

    private void Pinch()
    {
        if (Input.GetKey(KeyCode.J) && isAttacking == false)
        {
            rigidbodyPlayer.AddForce(Vector2.right * horizontalInput * jumpForce, ForceMode2D.Impulse);
            jPress = true;

        }
        else if (isAttacking)
        {
            jPress = false;
            rigidbodyPlayer.velocity = new Vector2(0, 0);
        }
    }

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
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
                rigidbodyPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else if (_rbSpeed == 0)
        {
            rigidbodyPlayer.gravityScale = 0f;
            isJumping = false;
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


}