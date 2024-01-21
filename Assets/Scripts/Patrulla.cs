using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrulla : MonoBehaviour
{
    [SerializeField] private Transform[] puntos;
    [SerializeField] private float VelPatrulla;
    [SerializeField] private bool Destino;
    private Vector3 destinoActual;
    private int indiceActual = 0;
    private bool jugadorDetectado = false;
    // Start is called before the first frame update
    void Start()
    {
        destinoActual = puntos[indiceActual].position;
        StartCoroutine(Patrullas());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Patrullas()
    {
        while (true)
        {

            while (transform.position != destinoActual)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinoActual, VelPatrulla * Time.deltaTime);
                yield return null;

            }

            DefinirNuevoDestino();

        }
    }

    private void DefinirNuevoDestino()
    {

        indiceActual++;
        if (indiceActual >= puntos.Length)
        {
            indiceActual = 0;

        }
        destinoActual = puntos[indiceActual].position;
        if (Destino)
        {
            EnfocarDestino();
        }
    }

    private void EnfocarDestino()
    {
        if (puntos.Length > 2)
        {

            if (indiceActual == 0 || indiceActual == (puntos.Length - 1))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (indiceActual > 0 && indiceActual < (puntos.Length - 1))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if (indiceActual == 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (indiceActual != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

    }

    public void ActivarDeteccionJugador()
    {
        jugadorDetectado = true;
    }
}
