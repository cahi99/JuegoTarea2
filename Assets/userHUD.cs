using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class userHUD : MonoBehaviour
{
    [SerializeField] private GameObject[] corazones;
    [SerializeField] private TextMeshProUGUI corazon_n;
    [SerializeField] private int saludActual;

    public void ActualizarVidas(int saludNuevo)
    {
        saludActual = saludNuevo;
        corazon_n.text = "";
        corazones[0].SetActive(false);
        corazones[1].SetActive(false);
        corazones[2].SetActive(false);
        if (saludActual > 0) {
            corazones[0].SetActive(true);
        }
        if(saludActual > 1) {
            corazones[1].SetActive(true);
        }
        if(saludActual > 2) {
            corazones[2].SetActive(true);
        }
        if(saludActual > 3) {
            corazones[0].SetActive(true);
            corazones[1].SetActive(false);
            corazones[2].SetActive(false);
            corazon_n.text = saludActual.ToString() + " X";
        }
    }
}
