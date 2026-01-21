using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontalInput;
    public float verticalInput;
    public float xSpeed;
    public float ySpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) //Detecta cuando pulsas 
        {
            transform.Translate(Vector2.right * Time.deltaTime * xSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * xSpeed);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * ySpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        { 
            transform.Translate(Vector2.down * Time.deltaTime * ySpeed);
        }


        /*
        horizontalInput = GetKeyDown(KeyCode.RightArrow); //Detecta cuando pulsas las flechas Izquierda / Derecha

        transform.Translate(Vector2.right * Time.deltaTime * xSpeed* horizontalInput);

        verticalInput = Input.GetAxis("Vertical"); //Detecta cuando pulsas las flechas Arriba / Abajo

        transform.Translate(Vector2.up * Time.deltaTime * xSpeed * verticalInput);
        */
    }
}
