using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    [Header("Salud jugador")]
    [SerializeField] private int vida = 3;
    [SerializeField] private int baseVida = 3;
    [SerializeField] private int maximoVida = 9;

    [Header("Control Daño")]
    [SerializeField] private float tiempoEntreDaño;
    [SerializeField] private float tiempoSiguienteDaño;

    // Start is called before the first frame update
    void Start()
    {
        vida = baseVida;
    }

    private void Update()
    {
        if (tiempoSiguienteDaño < 0.1f)
        {
            tiempoSiguienteDaño = 0;
        }
        else
        {
            tiempoSiguienteDaño -= Time.deltaTime;
        }
    }

    public void TomarDaño(int daño)
    {
        if(tiempoSiguienteDaño <= 0)
        {
            tiempoSiguienteDaño = tiempoEntreDaño;
            vida -= daño;
            gameObject.GetComponent<PlayerController>().ReboteDaño();
            if (vida <= 0)
            {
                Destroy(gameObject);
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
    }

}
