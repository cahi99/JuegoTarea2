using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguimiento : MonoBehaviour
{

    [SerializeField] public Transform jugador;
    [SerializeField] private float distancia;
    public Vector3 puntoInicial;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        puntoInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distancia = Vector2.Distance(transform.position , jugador.position);
        animator.SetFloat("Distancia", distancia);
    }

    public void Girar(Vector3 objetivo)
    {
        if (transform.position.x < objetivo.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX= false;
        }
    }
}
