using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES
    // INPUTS
    float horizontalInput; //almacena el valor del input horizontal (A y D, Flecha izda y Flecha Dcha)
    float verticalInput; //almacena el valor del input vertical (W y S, Flecha arriba y Flecha abajo)

    // VELOCIDADES
    public float speedMultiplier = 1; //Valor por el que multiplicar la velocidad base del player al correr
    public float xSpeed = 5; //Velocidad horizontal base del player.
    public float ySpeed = 3; //Velocidad vertical base del player.
    
    // USO DE COMPONENTES
    public SpriteRenderer spriteRenderer; //almacena el componente SpriteRenderer del player

    #endregion

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //Coge el spriteRenderer del player
    }

    void Update()
    {
        #region MOVEMENT

        // SPRINT
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMultiplier = 1.5f;
        }
        else
        {
            speedMultiplier = 1;
        }

        // MOVIMIENTO BASICO

        horizontalInput = Input.GetAxisRaw("Horizontal"); //Detecta cuando pulsas las flechas Izda / Dcha
        verticalInput = Input.GetAxisRaw("Vertical"); //Detecta cuando pulsas las flechas Arriba / Abajo

        transform.Translate(Vector2.right * Time.deltaTime * xSpeed * speedMultiplier * horizontalInput); //Mueve al player horizontalmente segun el horizontalInput y a la velocidad de xSpeed, ademas de multiplicarlo por el valor de speedmultiplier para que corra
        transform.Translate(Vector2.up * Time.deltaTime * ySpeed * speedMultiplier * verticalInput); //Mueve al player horizontalmente segun el verticalInput y a la velocidad de ySpeed, ademas de multiplicarlo por el valor de speedmultiplier para que corra

        CharacterFlip(); //Voltea al player para que mire hacia la direccion en que se mueve     

        #endregion MOVEMENT
    }


    private void CheckInputHorizontal() //almacena el valor del input horizontal
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void CharacterFlip() //Voltea al player para que mire hacia la direccion en que se mueve
    {
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0.01)
        {
            spriteRenderer.flipX = false;
        }
    }
}