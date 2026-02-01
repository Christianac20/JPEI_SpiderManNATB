using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject botonPausa;
        private bool juegoPausa = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (juegoPausa)
            {
                Reanudar();
            }
            else 
            {
                Pausa();
            }
    }
    public void Pausa()
    {
        juegoPausa = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        juegoPausa = false; 
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public void Salir(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);
    }



}
