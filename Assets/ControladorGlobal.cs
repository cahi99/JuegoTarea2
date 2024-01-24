using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControladorGlobal : MonoBehaviour
{
    [SerializeField] public static ControladorGlobal Instance;

    [Header("Stats")]
    [SerializeField] private float puntuacion;
    [SerializeField] private int salud;
    [SerializeField] private int vida;
    [SerializeField] public int nivel;

    [Header("Contador Tiempo")]
    [SerializeField] private float tiempo = 0;
    [SerializeField] private float segundos = 0;
    [SerializeField] private float minutos = 0;
    [SerializeField] private float horas = 0;
    [SerializeField] private TMP_Text timerText;

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

    private void Update()
    {
        tiempo += Time.deltaTime;
        minutos = (int)(tiempo / 60f);
        segundos = (int)(tiempo - minutos * 60f);
        horas = (int)(tiempo / 3600f);
        if (timerText!=null)
        {
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);
        }
    }

    public void colocarTimerText(TMP_Text timerTextNew)
    {
        timerText = timerTextNew;
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

    public void ActualizarNivel(int newNivel)
    {
        nivel = newNivel;
    }

}
