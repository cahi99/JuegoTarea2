using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorGlobal : MonoBehaviour
{
    public static ControladorGlobal Instance;

    [SerializeField] private float puntuacion;
    [SerializeField] private int salud;
    [SerializeField] private int vida;

    private void Awake()
    {
        if(ControladorGlobal.Instance == null)
        {
            ControladorGlobal.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SumarPuntos(float puntos)
    {
        puntuacion += puntos;
    }

    public void ActualizarVida(int puntos)
    {
        vida = puntos;
    }

    public void ActualizarSalud(int puntos)
    {
        salud = puntos;
    }

}
