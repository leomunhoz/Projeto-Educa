using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class playerOne : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    float Horizontal;
    float Vertical;
    public int attackDemage = 40;
    public int pulosExtras = 0;
    public int axPulosExtras = 0;
    public float attackDelay = 0.55f;
    public float attackRange = 0.5f;
    public Vector2 direction;
    private Vector2 directionanterior;

    private string currantState;

    public PlayerOneWayPlatform platform;

    #region Const Anim
    const string Idle = "Idle";
    const string Run = "Run";
    const string Jump = "Jump";
    const string JumptoFall = "JumptoFall";
    const string Fall = "Fall";
    const string Attack = "Attack";
    const string WallSliding = "Wall";


    #endregion


    public bool isGrounded;
    public bool isAttacking;
    public bool isAttackingPressed;
    public bool isWallSliding;
    public bool isFacingRigth = true;
    public float WallSlidingSpeed = 2f;
    public Transform attackPoint;
    public Transform groundPoint;
    public Transform wallCheck;
    public LayerMask WallLayer;
    public LayerMask GroundLayer;
    public LayerMask EnemyLayers;
   
   




    public Animator anim;

    

    void Start()
    {
        axPulosExtras = pulosExtras;
        rb2d = GetComponent<Rigidbody2D>();
        anim.GetComponent<Animator>();
        platform = GetComponent<PlayerOneWayPlatform>();

        


    }
    private void FixedUpdate()
    {
        Horizontal = rb2d.velocity.x;
        Vertical = rb2d.velocity.y;

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, GroundLayer);
        isWallSliding = Physics2D.OverlapCircle(wallCheck.position, 0.2f, WallLayer);


        attack();
        ParemetroDeAnim();
    }

    void Update()
    {

        Flip();
        WallSlide();
        
    }
    
 


    void Flip() 
    {
        
        if (isFacingRigth && Horizontal < 0 || !isFacingRigth && Horizontal > 0)
        {
            isFacingRigth = !isFacingRigth;
            Vector3 LocalScale = transform.localScale;
            LocalScale.x *= -1;
            transform.localScale = LocalScale;  
            
        }
    }
    void ParemetroDeAnim() 
    {
        if (!isAttacking && !isWallSliding)
        {
            if (Horizontal != 0 && isGrounded)
            {
                ChangeAnimState(Run);

            }
            else if (Vertical != 0 )
            {
                ChangeAnimState(Jump);
            }
            else
            {
                ChangeAnimState(Idle);
            }
        }
    }
    void attack()
    {
        if (isAttackingPressed)
        {

            if (!isAttacking)
            {
                isAttacking = true;
                if (isGrounded)
                {
                    rb2d.velocity = Vector2.up * 1;
                    ChangeAnimState(Attack);

                    Collider2D[] EnemyHits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayers);
                    foreach (var enemy in EnemyHits)
                    {
                        Debug.Log("Hit" + enemy.name);
                        enemy.GetComponent<Enemy>().TakeDemage(attackDemage);
                    }
                }
                Invoke("AttackComplete", attackDelay);

            }

        }
        else
            rb2d.velocity = new Vector2(direction.x * speed, Vertical);
    }
    

    public void WallSlide() 
    {
        if (isWallSliding && !isGrounded && Vertical != 0)
        {
            isWallSliding = true;
            ChangeAnimState(WallSliding);
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

    }
    public void DownPlatform(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (platform.currentOneWayPlatform != null)
            {
                platform.StartCoroutine(platform.DisableCollision());
            }
        }
    }
    public void jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded == true && !isWallSliding)
        {
            rb2d.velocity = Vector2.up * jumpForce;

        }
        if (context.performed && isGrounded == false && axPulosExtras > 0 && !isWallSliding)
        {
            rb2d.velocity = Vector2.up * jumpForce;

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
        if (attackPoint == null && wallCheck == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(wallCheck.position, 0.2f);
        Gizmos.DrawWireSphere(groundPoint.position, 0.1f);
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


















