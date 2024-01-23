using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Componentes")]
    public Rigidbody2D theRB;

    [Header("Movimiento")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDeMovimiento;
    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento;
    private Vector2 velocidad;
    public bool mirandoDerecha = true ;
    private float inputX;

    [Header("Salto")]
    //public float timeInverseJump = 0.2f;
    //public float timer = 1;
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] public Transform controladorSuelo;
    [SerializeField] public LayerMask queEsSuelo;
    [SerializeField] public Vector2 dimensionesCaja;
    [SerializeField] public bool enSuelo;
    [SerializeField] public int saltosRestantes;
    [SerializeField] public int saltos;
    [SerializeField] public int saltosPared;
    private bool salto = true;


    [Header("Detección de pared")]
    [SerializeField] private Transform controladorPared;
    [SerializeField] private Vector2 dimensionesCajaPared;
    private bool enPared;
    private bool deslizando;
    [SerializeField] private float velocidadDeslizar;
    [SerializeField] private float fuerzaSaltoParedX;
    [SerializeField] private float fuerzaSaltoParedY;
    [SerializeField] private float tiempoSaltoPared;
    [SerializeField] private bool saltandoDePared;

    [Header("Animación")]
    private Animator animator;

    [Header("Salto Regulable")]
    [Range(0, 1)][SerializeField] private float multiplicadorCancelarSalto;
    [SerializeField] private float multiplicadorGravedad;
    private float escalaGravedad;
    private bool botonSaltoArriba = true;

    [Header("Rebote")]
    [SerializeField] private float velocidadRebote;
    [SerializeField] private float velocidadReboteDaño=8;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        escalaGravedad = theRB.gravityScale;
    }

    
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        movimientoHorizontal = inputX * velocidadDeMovimiento;
        
        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));
        animator.SetFloat("VelocidadY", theRB.velocity.y);
        
        animator.SetBool("Deslizando", deslizando);

        if (enSuelo)
        {
            saltosRestantes = saltos;
        }

        if (Input.GetButton("Jump"))
        {
            salto = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            //boton de salto arriba
            BotonSaltoArriba();
        }

        if(!enSuelo && enPared && inputX !=0)
        {
            deslizando = true;
        }
        else
        {
            deslizando = false;
        }

    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo", enSuelo);
        
        enPared = Physics2D.OverlapBox(controladorPared.position, dimensionesCaja, 0f, queEsSuelo);

        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        
        if(theRB.velocity.y < 0 && !enSuelo)
        {
            theRB.gravityScale = escalaGravedad * multiplicadorGravedad;
        }
        else
        {
            theRB.gravityScale = escalaGravedad;
        }
        salto = false;

        if (deslizando)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, Mathf.Clamp(theRB.velocity.y, -velocidadDeslizar, float.MaxValue));
        }
    }

    private void Mover(float mover, bool saltar)
    {
        if(!saltandoDePared)
        {
            Vector2 velocidadObjetivo = new Vector2(mover, theRB.velocity.y);
            theRB.velocity = Vector2.SmoothDamp(theRB.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);
        }

        if (mover>0 && !mirandoDerecha)
        {
            Girar();
        }
        else if(mover<0 && mirandoDerecha)
        {
            Girar();
        }

        if(saltar && enSuelo && !deslizando && botonSaltoArriba && saltosRestantes > 0)
        {
            Salto();
            saltosRestantes--;
        }

        if(saltar && !enSuelo && !deslizando && botonSaltoArriba && saltosRestantes > 0)
        {
            Salto();
            saltosRestantes--;

        }

        if(saltar && enPared && deslizando && botonSaltoArriba && saltosPared > 0)
        {
            SaltoPared();
            saltosRestantes = saltosPared;

            saltosRestantes--;
        }
    }

    public void Rebote()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, velocidadRebote);
        saltosRestantes = 1;
    }

    public void ReboteDaño()
    {
        int dir = 1;
        if (mirandoDerecha)
        {
            dir *= -1;
        }
        StartCoroutine(CambioSaltoPared());
        theRB.velocity = new Vector2(dir * velocidadReboteDaño, velocidadReboteDaño);
        //theRB.AddForce(new Vector2(dir * velocidadReboteDaño, 0f));
        saltosRestantes = 1;
    }

    private void SaltoPared()
    {
        enPared = false;
        theRB.velocity = new Vector2(fuerzaSaltoParedX * -inputX, fuerzaSaltoParedY);
        StartCoroutine(CambioSaltoPared());
        salto = false;
        botonSaltoArriba = false;
    }

    private void Salto()
    {
        enSuelo = false;
        //theRB.AddForce(new Vector2(0f, fuerzaDeSalto));
        theRB.velocity = new Vector2(0f, fuerzaDeSalto);
        salto = false;
        botonSaltoArriba = false;
    }

    private void BotonSaltoArriba()
    {
        if (theRB.velocity.y>0)
        {
            theRB.AddForce(Vector2.down * theRB.velocity.y * (1 - multiplicadorCancelarSalto), ForceMode2D.Impulse);
        }

        botonSaltoArriba = true;
        salto = false;
    }

    IEnumerator CambioSaltoPared()
    {
        saltandoDePared = true;
        yield return new WaitForSeconds(tiempoSaltoPared);
        saltandoDePared = false;
    }

    IEnumerator waitDamage()
    {
        saltandoDePared = true;
        yield return new WaitForSeconds(1.2f);
        saltandoDePared = false;
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
        Gizmos.DrawWireCube(controladorPared.position, dimensionesCajaPared);
    }


}
