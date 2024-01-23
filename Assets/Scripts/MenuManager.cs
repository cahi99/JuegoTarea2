using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    private bool soundOn = true;
    [SerializeField] private GameObject menuPausa;

    public void Reanudar()
    {
        Time.timeScale = 1.0f;
        menuPausa.SetActive(false);
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

    public void Sonido()
    {
        AudioListener.pause = !AudioListener.pause;
        

    }
}
