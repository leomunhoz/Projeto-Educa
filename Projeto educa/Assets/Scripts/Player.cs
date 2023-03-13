using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    private Vector2 direction;
    public static player instance;



   

    public bool taNoChao;
    public Transform detectaChao;
    public LayerMask chao;
    Vector2 dir;
    Vector2 esq;
    string[] Anim = { "Idle", "Run", "Jump", "Fall", "Attack" };

    public int pulosExtras = 0;
    public int axPulosExtras = 0;

    public Animator anim;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        axPulosExtras = pulosExtras;
        rb2d = GetComponent<Rigidbody2D>();
        anim.GetComponent<Animator>();
        dir = transform.localScale;
        esq = transform.localScale;
        esq.x = esq.x * -1;


    }


    void Update()
    {
        taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.3f, chao);
        rb2d.velocity = new Vector2(direction.x * speed, rb2d.velocity.y);

        if (rb2d.velocity.x > 0)
        {
            transform.localScale = dir;
        }
        else if (rb2d.velocity.x < 0)
        {
            transform.localScale = esq;
        }

        if (rb2d.velocity.y > 0)
        {
            AnimMovement("Jump");
        }
        else if (rb2d.velocity.y < 0)
        {
            AnimMovement("Fall");
        }
        else if (rb2d.velocity.x > 0)
        {
            AnimMovement("Run");
        }
        else if (rb2d.velocity.x < 0)
        {
            AnimMovement("Run");
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            AnimMovement("Attack");
        }
        else
        {
            AnimMovement("Idle");
        }


    }




    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (context.performed && taNoChao == true)
        {
            rb2d.velocity = Vector2.up * 6;
        }
        if (context.performed && taNoChao == false && axPulosExtras > 0)
        {
            rb2d.velocity = Vector2.up * 6;

            axPulosExtras--;
        }
    }

    public void attack(InputAction.CallbackContext context) 
    {

           
        

    }



    void AnimMovement(string animCond)
{
    foreach (var item in Anim)
    {
        anim.SetBool(item, false);

    }
    anim.SetBool(animCond, true);
}

}

        
       
       


       
   