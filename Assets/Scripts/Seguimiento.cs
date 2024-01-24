using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class Seguimiento : MonoBehaviour
{

    [SerializeField] public Transform jugador;
    [SerializeField] private float distancia;
    public Vector3 puntoInicial;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Transform pinicial;
    public Pathfinding.AIDestinationSetter destinationSetter;
    public Pathfinding.AILerp AILerp;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        puntoInicial = transform.position;
        pinicial.transform.position = puntoInicial;
        destinationSetter = GetComponent<AIDestinationSetter>();
        AILerp = GetComponent<AILerp>();
    }

    // Update is called once per frame
    void Update()
    {
        if(jugador != null)
        {
            distancia = Vector2.Distance(transform.position, jugador.position);
            animator.SetFloat("Distancia", distancia);
        }

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
