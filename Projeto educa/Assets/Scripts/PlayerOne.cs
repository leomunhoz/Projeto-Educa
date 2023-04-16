using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class playerOne : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float Horizontal;
    [SerializeField] private float Vertical;
    [SerializeField] private int attackDemage = 40;
    [SerializeField] private int pulosExtras = 1;
    [SerializeField] private int axPulosExtras = 1;
    [SerializeField] private float attackDelay = 0.3f;
    [SerializeField] private float attackRange = 0.1f;
    [SerializeField] private int combo;

    private Vector2 direction;



    private string currantState;

    public PlayerOneWayPlatform platform;

    #region Const Anim
    const string Idle = "Idle";
    const string Run = "Run";
    const string Jump = "Jump";
    const string JumptoFall = "JumptoFall";
    const string Fall = "Fall";
    const string Attack = "Attack 1";
    const string WallSliding = "SlideWall";
    const string Down = "Down";


    #endregion


    
     private bool isGrounded;
     private bool isAttacking;
     private bool isAttackingPressed;
     private bool isJumpingPressed;
     private bool isRollingPressed;
     private bool isSkeyDownPress;
     private bool isMousePress;
     private bool isJumping;
     private bool isWallSliding;
   

    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    public bool wallJumping;



    [SerializeField] private bool isFacingRigth = true;
    [SerializeField] private float WallSlidingSpeed = 2f;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask WallLayer;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask EnemyLayers;



    public Animator anim;

    [SerializeField] private AnimationClip[] Animcombo;
    [SerializeField] private float duraçãoDocombo = 1f;
    [SerializeField] private float tempoMax;
    [SerializeField] private float tempParaProximoAtaque;
    int animAtual = 0;

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

        attack();
        ParemetroDeAnim();
        WallSlide();
        jump();
       
        DownPlat();


    }
    void Update()
    {

        isJumpingPressed = Gamepad.current.buttonSouth.isPressed || Keyboard.current.spaceKey.isPressed;
        isAttackingPressed = Gamepad.current.buttonNorth.isPressed || Keyboard.current.fKey.isPressed;
        isRollingPressed = Gamepad.current.buttonEast.isPressed;
        isSkeyDownPress = Keyboard.current.sKey.isPressed;
        isMousePress = Mouse.current.leftButton.isPressed;

        //Debug.Log(direction.y);
        Flip();
        wallJump();



        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, GroundLayer);
        isWallSliding = Physics2D.OverlapCircle(wallCheck.position, 0.2f, WallLayer);

    }
    public void Flip()
    {

        if (isFacingRigth && Horizontal < 0 || !isFacingRigth && Horizontal > 0)
        {
            isFacingRigth = !isFacingRigth;
            Vector3 LocalScale = transform.localScale;
            LocalScale.x *= -1;
            transform.localScale = LocalScale;

        }
    }
    public void attack()
    {
        if (isAttackingPressed || isMousePress)
        {

            if (!isAttacking)
            {
                isAttacking = true;
                if (isGrounded)
                {
                    rb2d.velocity = Vector2.up * 1;
                    if (Time.time > tempParaProximoAtaque)
                    {
                        anim.Play(Animcombo[animAtual].name);
                        tempParaProximoAtaque = Time.time + tempoMax;
                        animAtual++;
                        if (animAtual >= Animcombo.Length)
                        {
                            animAtual = 0;
                        }
                    }
                    if (Time.time > tempParaProximoAtaque + duraçãoDocombo)
                    {
                        tempParaProximoAtaque = Time.time;
                        animAtual = 0;
                    }




                    Collider2D[] EnemyHits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayers);
                    foreach (var enemy in EnemyHits)
                    {
                        Debug.Log("Hit" + enemy.name);
                        enemy.GetComponent<Inimigo>().TakeDemage(attackDemage);
                    }
                }
                Invoke("AttackComplete", attackDelay);

            }

        }
        else
        {
            Move();
        }
        

    }
    void Move() 
    {
        if (!isAttackingPressed)
        {
            rb2d.velocity = new Vector2(direction.x * speed, Vertical);
        }
       
    }
    public void jump()
    {

        if (isJumpingPressed && !(direction.y < 0))
        {

            if (!isJumping && isGrounded == true && !isWallSliding)
            {
                rb2d.velocity = Vector2.up * jumpForce;
            }
            else if (isJumping && isGrounded == false && axPulosExtras > 0 && !isWallSliding)
            {
                rb2d.velocity = Vector2.up * jumpForce;

                axPulosExtras--;
            }


            isJumpingPressed = false;
        }






    }
    public void DownPlat()
    {
        if (direction.y < -0.90 && isGrounded)
        {
            ChangeAnimState(Down);
            speed = 0;
            Debug.Log(speed);
            if (direction.y < 0 && platform.isPlatformDownPressed || isSkeyDownPress)
            {
                platform.isPlatformDownPressed = true;

                if (platform.currentOneWayPlatform != null)
                {
                    platform.StartCoroutine(platform.DisableCollision());
                }
            }
        }
        else
        {
            speed = 7;
        }
       
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
            ParemetroDeAnim();


        }
    }
    public void wallJump()
    {
        if (isWallSliding)
        {
            wallJumping = true;
            Invoke("StopWallJumping", wallJumpDuration);
        }
        if (isJumpingPressed)
        {
            if (wallJumping)
            {
                rb2d.velocity = new Vector2(-wallJumpForce.x, wallJumpForce.y);

            }
           

        }
    }
    public void Move(InputAction.CallbackContext context)
    {

        direction = context.ReadValue<Vector2>();


    }

  
    void AttackComplete()
    {
        isAttacking = false;
        isAttackingPressed = false;
    }
    void StopWallJumping()
    {
        wallJumping = false;

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null && wallCheck == null && groundPoint == null)
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
    public void ParemetroDeAnim()
    {
        if (!isAttacking && !isWallSliding && !(direction.y < 0))
        {
            if (Horizontal != 0 && isGrounded)
            {
                ChangeAnimState(Run);

            }
            else if (Vertical != 0)
            {
                ChangeAnimState(Jump);
            }
            else
            {
                ChangeAnimState(Idle);
            }
        }
    }

}




























































