using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDerrota : MonoBehaviour
{
    [SerializeField] private GameObject pantallaMenuDerrota;

    public void Update()
    {
        if (pantallaMenuDerrota.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
    }


    public void ReiniciarElNivel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Carga nuevamente la misma escena
        SceneManager.LoadScene(currentSceneName);
    }

    public void MenuInicial(int nro)
    {
        SceneManager.LoadScene(nro);
    }
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

}
