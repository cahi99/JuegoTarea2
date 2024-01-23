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
        textboton = Jugar.GetComponent<Text>();
        textboton.text = "Continuar";
    }

    private void Update()
    {
       if(jugador != null)
        {
         textboton.text = "Reintentar";
            Jugar.onClick.AddListener(() => Reiniciar());
        }
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
