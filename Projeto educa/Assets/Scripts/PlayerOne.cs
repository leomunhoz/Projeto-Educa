using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerOne : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public int pulosExtras = 0;
    public int axPulosExtras = 0;
    public float attackDelay = 0.55f;
    public float attackRange = 0.5f;
    private Vector2 direction;
    private Vector2 directionanterior;
    
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
    public Transform attackPoint;
    public int attackDemage = 40;
    public LayerMask chao;
    public LayerMask EnemyLayers;
    Vector2 dir;
    Vector2 esq;
    
    


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
       

        if (isAttackingPressed)
        {
            
            if (!isAttacking)
            {
                isAttacking = true;
                if (taNoChao)
                {
                    rb2d.velocity = Vector2.up * 1;
                    ChangeAnimState(Attack);
                    
                    Collider2D[] EnemyHits = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,EnemyLayers);
                    foreach (var enemy in EnemyHits)
                    {
                        Debug.Log("Hit" + enemy.name);
                        enemy.GetComponent<Enemy>().TakeDemage(attackDemage);
                    }
                }
                Invoke("AttackComplete", 0.55f);
                
            }
            
        }
        else
            rb2d.velocity = new Vector2(direction.x * speed, rb2d.velocity.y);
        if (rb2d.velocity.x > 0)
        {
            transform.localScale = dir;
        }
        else if (rb2d.velocity.x < 0)
        {
            transform.localScale = esq;
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
        isAttackingPressed = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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

        
       
       


       
   