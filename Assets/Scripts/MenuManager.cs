using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    private bool soundOn = true;
    [SerializeField] private GameObject menuPausa;
    public Button Jugar;
    public string reintentar = "Reintentar";
    public GameObject jugador;
    private Text textboton;



    public void Reanudar()
    {
        Time.timeScale = 1.0f;
        menuPausa.SetActive(false);

    }

    private void Start()
    {
        
    }

    private void Update()
    {
       
    }


    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
