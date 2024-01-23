using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    [Header("Salud jugador")]
    [SerializeField] private int vida = 3;
    [SerializeField] private int baseVida = 5;
    [SerializeField] private int maximoVida = 9;
    [SerializeField] private Canvas HUD;
    [SerializeField] private GameObject creditos;

    [Header("Control Da�o")]
    [SerializeField] private float tiempoEntreDa�o;
    [SerializeField] private float tiempoSiguienteDa�o;

    // Start is called before the first frame update
    void Start()
    {
        vida = baseVida;
        HUD.GetComponent<userHUD>().ActualizarVidas(vida);
    }

    private void Update()
    {
        if (tiempoSiguienteDa�o < 0.1f)
        {
            tiempoSiguienteDa�o = 0;
        }
        else
        {
            tiempoSiguienteDa�o -= Time.deltaTime;
        }
    }

    public void TomarDa�o(int da�o)
    {
        if(tiempoSiguienteDa�o <= 0)
        {
            tiempoSiguienteDa�o = tiempoEntreDa�o;
            vida -= da�o;
            gameObject.GetComponent<PlayerController>().ReboteDa�o();
            HUD.GetComponent<userHUD>().ActualizarVidas(vida);
            if (vida <= 0)
            {
                Destroy(gameObject);
                creditos.SetActive(true);
            }
        }
    }

    public void Curar(int curacion)
    {
        if((vida + curacion)> maximoVida)
        {
            vida = maximoVida;
        }
        else
        {
            vida += curacion;
        }
        HUD.GetComponent<userHUD>().ActualizarVidas(vida);
    }

    public void saveLife()
    {
        ControladorGlobal.Instance.ActualizarSalud(vida);
    }
}
