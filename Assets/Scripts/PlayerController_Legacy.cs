using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Legacy : MonoBehaviour
{
    // Variables Float
    public float horizontalInput;
    public float verticalInput;
    public float baseSpeedX;
    public float baseSpeedY;
    public float xSpeed;
    public float ySpeed;
    public float speedMultiplierX;
    public float speedMultiplierY;
    public float umbralTiempo;
    private float TiempoCargado;
    public float jumpForce = 1000;
    public float _rbSpeed;
    public float posibleJumps;
    public float currentJumps;
    // Varialbes Bool
    private bool jPress;
    private bool jHold;
    private bool isAttacking;
    private bool isCharge;
    private bool isJumping;
    private bool Grounded;
    //Variables de Componente
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D _rbPlayer;
    [SerializeField] LayerMask Ground;
    //Variables Compuestas
    private Vector2 movement;
    private RaycastHit2D groundHit;

    void Start()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (groundHit == true)
        {
            Debug.Log("Esmegma");
        }

        //groundHit = Physics2D.Raycast(transform.position, Vector2.down, 1f, Ground);

        //Actualizamos la velocidad del Rigidbody cada frame
        _rbSpeed = _rbPlayer.velocity.magnitude;

        //Cambiamos elvalor del Movement
        movement = new Vector2(horizontalInput, verticalInput);

        //Corregimos la orientación del sprite
        SpriteFlip();

        //Miramos si está atacando
        AnimationTagCheck();
        #region MOVEMENT.MODIFIERS
        //Nos aseguramos de que este pulsando o no el botón de correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            xSpeed = baseSpeedX * speedMultiplierX;
            ySpeed = baseSpeedY * speedMultiplierY;
        }
        else
        {
            xSpeed = baseSpeedX;
            ySpeed = baseSpeedY;
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

            transform.Translate(Vector2.right * Time.deltaTime * xSpeed * horizontalInput);

            verticalInput = Input.GetAxisRaw("Vertical"); //Detecta cuando pulsas las flechas Arriba / Abajo

            transform.Translate(Vector2.up * Time.deltaTime * xSpeed * verticalInput);
        }

        #endregion

        #region AnimatorBools
        animator.SetBool("Jump", isJumping);
        animator.SetBool("Attack", isAttacking);
        animator.SetBool("Charge", isCharge);
        animator.SetBool("JPress", jPress);
        animator.SetBool("JHold", jHold);
        animator.SetBool("IdleCrab", movement == Vector2.zero);
        animator.SetFloat("VelocidadCrabX", xSpeed);
        #endregion

        /*horizontalInput = Input.GetAxisRaw("Horizontal"); //Detecta cuando pulsas las flechas Izquierda / Derecha

         transform.Translate(Vector2.right * Time.deltaTime * xSpeed * horizontalInput);

         verticalInput = Input.GetAxisRaw("Vertical"); //Detecta cuando pulsas las flechas Arriba / Abajo

         transform.Translate(Vector2.up * Time.deltaTime * xSpeed * verticalInput);
         #endregion

         animator.SetBool("IdleCrab", movement == Vector2.zero);
         animator.SetFloat("VelocidadCrabX", xSpeed);
        */
    }

    private void HorizontalImputCheck()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void SpriteFlip()
    {
        if (horizontalInput > 0.01)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput < -0.01)
        {
            spriteRenderer.flipX = false;
        }
    }
    /*
    void JAttack()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            umbralTiempo = 1f;
            TiempoCargado = Time.deltaTime;
            isCharge = true;

            if(isAttacking == true)
            {
                if (TiempoCargado > umbralTiempo && isCharge == true)
                {
                    Contusion();
                }
                else
                {
                    Pinch();
                }
            }
        }
        
            
    }

    private void Contusion()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            jHold = true;
        }
        else
        {
            jHold = false;
        }
    }*/
    private void Pinch()
    {
        if (Input.GetKey(KeyCode.J) && isAttacking == false)
        {
            _rbPlayer.AddForce(Vector2.right * horizontalInput * jumpForce, ForceMode2D.Impulse);
            jPress = true;

        }
        else if (isAttacking)
        {
            jPress = false;
            _rbPlayer.velocity = new Vector2(0, 0);
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && currentJumps < posibleJumps)
        {
                _rbSpeed = 0;
                _rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                _rbPlayer.gravityScale = 2f;
                currentJumps += 1;
        }
        else if (_rbSpeed == 0)
        {
            _rbPlayer.gravityScale = 0f;
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

