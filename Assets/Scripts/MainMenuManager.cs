using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Agregar libreria para manejar elementos de escenas
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Metodo que direcciona a la escena de juego con ese nombre (sincrono)
    public void ChangeScene(string sceneName)
    {
        // direcciona a la escena con el nombre que se le pasa como parametro
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(int sceneIndex)
    {
        // direcciona a la escena con el indice que se le pasa como parametro
        SceneManager.LoadScene(sceneIndex);
    }

    // Metodo que cierra la aplicacion
    public void ExitGame()
    {
        // Cierra la aplicacion
        Application.Quit();
        Debug.Log("Cierra la ventana de juego.");
    }

    // Metodo para llamar la corutina de carga de escena (async)
    public void CallExampleCoroutine()
    {
        //StartCoroutine(LoadSceneAsync(sceneName));
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        Debug.Log("Inicio de la corrutina. Este codigo se ejecuta primero.");
        yield return new WaitForSeconds(5f);
        Debug.Log("Pasa 5 segundos. Este codigo se ejecuta despues de 5 segundos del sistema.");
        yield return new WaitForSecondsRealtime(4f);
        Debug.Log("Pasa 4 segundos. Este codigo se ejecuta despues de 4 segundos reales.");
    }
}
