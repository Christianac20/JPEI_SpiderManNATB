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
    public float speedMultiplier = 1.5f; //Valor por el que multiplicar la velocidad base del player al correr
    public float xSpeedBase = 5; //Velocidad horizontal base del player. Constante
    [SerializeField] float xSpeed; //Velocidad horizontal base del player. Variable
    public float ySpeedBase = 3; //Velocidad verticalbase del player. Constante
    [SerializeField] float ySpeed; //Velocidad vertical base del player. Variable
    
    // USO DE COMPONENTES
    public SpriteRenderer spriteRenderer; //almacena el componente SpriteRenderer del player

    #endregion

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //Coge el spriteRenderer del player
        xSpeed = xSpeedBase; //Asigna la velocidad x 
        ySpeed = ySpeedBase; //Asigna la velocidad y
    }

    void Update()
    {
        #region MOVEMENT

        // SPRINT

        if (Input.GetKey(KeyCode.LeftShift))
        {
            xSpeed = xSpeedBase * speedMultiplier; //multiplica la velocidad en X al correr
            ySpeed = ySpeedBase * speedMultiplier; //multiplica la velocidad en Y al correr
        }
        else
        {
            ySpeed = ySpeedBase; //resetea la velocidad en X a su valor Base
            xSpeed = xSpeedBase; //resetea la velocidad en Y a su valor Base
        }

        // MOVIMIENTO BASICO

        horizontalInput = Input.GetAxisRaw("Horizontal"); //Detecta cuando pulsas las flechas Izda / Dcha
        verticalInput = Input.GetAxisRaw("Vertical"); //Detecta cuando pulsas las flechas Arriba / Abajo

        transform.Translate(Vector2.right * Time.deltaTime * xSpeed * horizontalInput); //Mueve al player horizontalmente segun el horizontalInput y a la velocidad de xSpeed
        transform.Translate(Vector2.up * Time.deltaTime * ySpeed * verticalInput); //Mueve al player horizontalmente segun el verticalInput y a la velocidad de ySpeed


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