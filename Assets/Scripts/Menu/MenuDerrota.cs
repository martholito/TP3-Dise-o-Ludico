#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDerrota : MonoBehaviour
{
    [SerializeField] private GameObject pantallaMenuDerrota;

    private void Update()
    {
        // Verifica si el menú de derrota está activo y ajusta el estado del tiempo y el cursor
        if (pantallaMenuDerrota.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ReiniciarElNivel()
    {
        // Restaurar el tiempo y recargar la escena actual
        Time.timeScale = 1f;
        //GameManager.instance.SetCurrentHealth(100);
        //GameManager.instance.SetCurrentBatery(100);

        // Asegúrate de ocultar el menú de derrota antes de recargar
        pantallaMenuDerrota.SetActive(false);

        // Obtener y recargar la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log($"Recargando la escena: {currentSceneName}");
        SceneManager.LoadScene(currentSceneName);
    }

    public void MenuInicial(int nro)
    {
        // Restaurar el tiempo y cargar el menú principal
        Time.timeScale = 1f;
        SceneManager.LoadScene(nro);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode(); //Solo editor, no funciona en la build!!!
#endif
    }
}
