using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    //[SerializeField] private GameObject efecto;

    private Animator animator;

    [SerializeField] private int salud = 3;

    [Header("Control Golpe")]
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private int dañoGolpe;
    [SerializeField] private bool rebota=true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<CombateJugador>().TomarDaño(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("contacto1");
            if (collision.GetContact(0).normal.y <= -0.5 && rebota)
            {
                animator.SetTrigger("Hit");
                collision.gameObject.GetComponent<PlayerController>().Rebote();
                TomarDaño(1);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if ( !(collision.GetContact(0).normal.y <= -0.5) )
            {
                collision.gameObject.GetComponent<CombateJugador>().TomarDaño(1);
            }
        }
    }

    public void TomarDaño(int daño)
    {
        salud -= daño;
        if (salud <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(controladorGolpe.position, dimensionesCaja, 0f);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.gameObject.CompareTag("Player"))
            {
                colisionador.transform.GetComponent<CombateJugador>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorGolpe.position, dimensionesCaja);
    }
}
