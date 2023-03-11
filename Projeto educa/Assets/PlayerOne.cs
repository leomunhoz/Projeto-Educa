using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerOne : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    private Vector2 direction;
    public static player instance;
    private string currantState;

    #region Const Anim
    const string Idle = "Idle";
    const string Run = "Run";
    const string Jump = "Jump";
    const string JumptoFall = "JumptoFall";
    const string Fall = "Fall";
    const string Attack = "Attack";


    #endregion


    public bool taNoChao;
    public bool isAttacking;
    public bool isAttackingPressed;
    public Transform detectaChao;
    public LayerMask chao;
    Vector2 dir;
    Vector2 esq;
    

    public int pulosExtras = 0;
    public int axPulosExtras = 0;
    public float attackDelay = 0.3f;

    public Animator anim;


   

    void Start()
    {
        axPulosExtras = pulosExtras;
        rb2d = GetComponent<Rigidbody2D>();
        anim.GetComponent<Animator>();
        dir = transform.localScale;
        esq = transform.localScale;
        esq.x = esq.x * -1;


    }
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.red, 10f);
        taNoChao = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, chao);
        //taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.3f, chao);

        if (!isAttacking)
        {
            if (rb2d.velocity.x != 0 && taNoChao)
            {
                ChangeAnimState(Run);
            }
           else if (rb2d.velocity.y != 0)
            {
                ChangeAnimState(Jump);
            }
            else
            {
                ChangeAnimState(Idle);
            }
        }
    }

    void Update()
    {
       // taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.3f, chao);
       
        rb2d.velocity = new Vector2(direction.x * speed, rb2d.velocity.y);
        
        
        if (rb2d.velocity.x > 0)
        {
            transform.localScale = dir;
        }
        else if (rb2d.velocity.x < 0)
        {
            transform.localScale = esq;
        }

        
       
        

       
        

        if (isAttackingPressed)
        {
            isAttackingPressed = false;
            if (!isAttacking)
            {
                isAttacking = true;
                if (taNoChao)
                {
                    ChangeAnimState(Attack);
                }
               
                Invoke("AttackComplete", 0.55f);
              
            }
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
        if (context.started)
        {
            isAttackingPressed = true;
        }
    } 
        

    

    void AttackComplete() 
    {
        isAttacking = false;
    }

    void ChangeAnimState(string newState)
    {
        if (currantState == newState)
        {
            return;
        }
        anim.Play(newState);
    }

}

        
       
       


       
   