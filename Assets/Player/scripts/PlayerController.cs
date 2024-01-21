using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed;

    [Header("Salto")]
    public float timeInverseJump = 0.2f;
    public float timer = 1;
    public bool isWallOverlap;
    public int numJump = 0;
    public float jumpForce;
    public bool stayDirJump = false;
    public float dirJump;

    [Header("Componentes")]
    public Rigidbody2D theRB;

    [Header("Detección de piso")]
    public bool isGrounded;

    public Transform groundCheckpoint;

    public LayerMask whatIsGround;

    void Start()
    {
        
    }

    
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))

        /*isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, .2f, whatIsGround);
        if (isGrounded)
        {
            numJump = 2;
        }*/
        float horizontalDireccion = 1;
        if(dirJump != Input.GetAxisRaw("Horizontal"))
        {
            stayDirJump = false;
        }

        float ydirection = theRB.velocity.y;

        if (Input.GetButtonDown("Jump"))
        {
            print("dirJump");
            print(dirJump);
            print("horizontal");
            print(Input.GetAxisRaw("Horizontal"));
            isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, .2f, whatIsGround);
            bool canJump = true;
            if (isWallOverlap && !isGrounded)
            {
                if (Input.GetAxisRaw("Horizontal") != 0 && dirJump != Input.GetAxisRaw("Horizontal"))
                {
                    dirJump = Input.GetAxisRaw("Horizontal");
                    numJump = 2;
                    stayDirJump = true;
                    timeInverseJump = 0.2f;
                    timer = 0;
                }
                else
                {
                    canJump = false;
                }
            }
            if (canJump)
            {
            print("numJump");
            print(numJump);
                if (numJump > 0)
                {
                    numJump -= 1;
                    ydirection = jumpForce;

                }
            }
                
            /*if (isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                numExtraJump = 1;
            }
            else
            {
                if (numExtraJump > 0)
                {
                    numExtraJump-= 1;

                }
            }*/
        }
        if (timer < timeInverseJump && stayDirJump)//make inverse direccion by time jump
        {
            isWallOverlap = false;
            horizontalDireccion = -1;
            timer += Time.deltaTime;
        }
        else
        {
            horizontalDireccion = 1;
        }
        theRB.velocity = new Vector2(horizontalDireccion * moveSpeed * Input.GetAxisRaw("Horizontal"), ydirection) ;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, .2f, whatIsGround);
            if (isGrounded)
            {
                numJump = 1;
                dirJump = 0;
                isWallOverlap = false;
            }
            else
            {
                isWallOverlap = true;
                //if (dirJump != Input.GetAxisRaw("Horizontal") && Input.GetAxisRaw("Horizontal") != 0 && dirJump != 0)
                /*if (dirJump == 0 || ( dirJump != Input.GetAxisRaw("Horizontal") && Input.GetAxisRaw("Horizontal") != 0 ) )
                {
                    isWallOverlap = true;
                }
                else
                {
                    //dirJump = Input.GetAxisRaw("Horizontal");
                    isWallOverlap = false;
                    //numJump = 0;
                }*/
            }

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isWallOverlap = false;
        /*if (isWallOverlap && dirJump != Input.GetAxisRaw("Horizontal") && dirJump !=0)
        {
            numJump = 2;

        }*/
    }

}
