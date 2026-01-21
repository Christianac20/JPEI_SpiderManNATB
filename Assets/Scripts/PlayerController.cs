using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    public float xSpeed;
    public float ySpeed;
    public SpriteRenderer spriteRenderer;


    // Update is called once per frame
    void Update()
    {
        #region MOVEMENT

        horizontalInput = Input.GetAxisRaw("Horizontal"); //Detecta cuando pulsas las flechas Izda / Dcha
        verticalInput = Input.GetAxisRaw("Vertical"); //Detecta cuando pulsas las flechas Arriba / Abajo

        transform.Translate(Vector2.right * Time.deltaTime * xSpeed * horizontalInput); //Mueve al player horizontalmente segun el horizontalInput y a la velocidad de xSpeed
        transform.Translate(Vector2.up * Time.deltaTime * ySpeed * verticalInput); //Mueve al player horizontalmente segun el verticalInput y a la velocidad de ySpeed


        CharacterFlip();     

        #endregion MOVEMENT
    }


    private void CheckInputHorizontal()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void CharacterFlip()
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