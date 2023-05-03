using Edgar.GraphBasedGenerator.Common.ChainDecomposition;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Controls;
using UnityEditor;

public class PlayerOne : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float Horizontal;
    private float Vertical;
    [SerializeField] private int attackDemage;
    [SerializeField] private int pulosExtras = 1;
    [SerializeField] private int axPulosExtras = 1;
    [SerializeField] private float attackDelay = 0.3f;
    [SerializeField] private float attackRange = 0.1f;
    [SerializeField] private int combo;
    public int currentHealth;
    public bool isChain;
    public bool isClimbing;


    public Vector2 direction;



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
    const string Death = "Death";
    const string Climb = "Climb";


    #endregion



    public bool isGrounded;
    private bool isAttacking;
    private bool isAttackingPressed;
    private bool isJumpingPressed;
    private bool isRollingPressed;
    private bool isSkeyDownPress;
    private bool isMousePress;
    private bool isJumping;
    private bool isWallSliding;
    private bool isDead;
    private bool IMORTAL=false;


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
    public int mortos;
    public int vida;
    public int defesa;

    GameObject[] PlayerVida;

   

    public void Parametros(int contMortos,int danoH, int vidaH, int defesaH)
    {
        mortos = contMortos;
        attackDemage = danoH;
        vida = vidaH;
        defesa = defesaH;
    }

    void Start()
    {
        
        currentHealth = vida;
        axPulosExtras = pulosExtras;
        rb2d = GetComponent<Rigidbody2D>();
        anim.GetComponent<Animator>();
        platform = GetComponent<PlayerOneWayPlatform>();

       PlayerVida = GameObject.FindGameObjectsWithTag("Life");


    }
    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, GroundLayer);
        isWallSliding = Physics2D.OverlapCircle(wallCheck.position, 0.2f, WallLayer);
        Horizontal = rb2d.velocity.x;
        Vertical = rb2d.velocity.y;

        attack();
        WallSlide();
        jump();
        
        
        


    }
    void Update()
    {
        
        isJumpingPressed = Gamepad.current.buttonSouth.isPressed || Keyboard.current.spaceKey.isPressed;
        isAttackingPressed = Gamepad.current.buttonNorth.isPressed || Keyboard.current.fKey.isPressed;
        isRollingPressed = Gamepad.current.buttonEast.isPressed;
        isSkeyDownPress = Keyboard.current.sKey.isPressed;
        
        //isMousePress = Mouse.current.leftButton.isPressed;
        wallJump();
        Flip();
        DownPlat();
        ParemetroDeAnim();
        Climbing();
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {

            if (!IMORTAL)
            {
                print("Imortal");
                IMORTAL = true;
            }
            else
            {
                print("Mortal");
                IMORTAL = false;
            }
                
        }
            





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
        if (isAttackingPressed || isMousePress && !isDead)
        {

            if (!isAttacking)
            {
                isAttacking = true;
                if (isGrounded)
                {
                    rb2d.velocity = new Vector2(direction.x * 0, Vertical);
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
                        //Debug.Log("Hit" + enemy.name);
                        enemy.GetComponent<Inimigo>().TakeDemage(attackDemage);
                    }
                }
                StartCoroutine(AttackComplete());

            }

        }
        else
        {
            Move();
        }


    }
    void Move()
    {
        if (!isAttackingPressed && !isSkeyDownPress || !platform.isPlatformDownPressed && isDead)
        {
            rb2d.velocity = new Vector2(direction.x * speed, Vertical);
        }

    }
    public void jump()
    {

        if (isJumpingPressed && !(direction.y < 0) && !isDead)
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
        if (direction.y < -0.55 && isGrounded)
        {
            ChangeAnimState(Down);
            speed = 0;
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
        if (isWallSliding && !isGrounded && Vertical == 0)
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
            
            StartCoroutine(StopWallJumping());
        }
        if (isJumpingPressed)
        {
            if (wallJumping && !isGrounded)
            {
                float currentTime = 0;
                float time = currentTime / wallJumpDuration;
                //rb2d.velocity = new Vector2(wallJumpForce.x * speed, wallJumpForce.y);
                if (isFacingRigth)
                {
                    rb2d.AddForce(new Vector2(Mathf.Lerp(-wallJumpForce.x, 0, time), Mathf.Lerp(wallJumpForce.y, 0, time)));
                }
                else
                {
                    rb2d.AddForce(new Vector2(Mathf.Lerp(wallJumpForce.x, 0, time), Mathf.Lerp(wallJumpForce.y, 0, time)));
                }

            }
           


        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (!isDead)
        {
            direction = context.ReadValue<Vector2>();
        }
      


    }


    IEnumerator AttackComplete()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        isAttackingPressed = false;
    }
    IEnumerator StopWallJumping()
    {
        yield return new WaitForSeconds(wallJumpDuration);
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
    public void ChangeAnimState(string newState)
    {
        if (currantState == newState)
        {
            return;
        }
        anim.Play(newState);
    }
    public void ParemetroDeAnim()
    {
        if (!isAttacking && !isWallSliding && !(direction.y < 0) && !isDead)
        {
            if (Horizontal != 0 && isGrounded && !isClimbing)
            {
                ChangeAnimState(Run);

            }
            else if (Vertical != 0 && !isClimbing)
            {
                ChangeAnimState(Jump);
            }
            else
            {
                if (!isClimbing)
                    ChangeAnimState(Idle);
            }
        }
    }

  
    public void TakeDamage(int damage) 
    {
        if (!IMORTAL)
            currentHealth = currentHealth -(damage - defesa);
        for (int i = 0; i < PlayerVida.Length; i++)
        {
            if (currentHealth >= (i + 1) * 10)
            {
                PlayerVida[i].SetActive(true);
            }
            else
            {
                PlayerVida[i].SetActive(false);
            }
        }
        if (currentHealth <= 0)
        {
            isDead = true;
            if (isDead)
            {
                ChangeAnimState(Death);
                speed = 0;
                jumpForce = 0;
                rb2d.gravityScale = 0;
                StartCoroutine(SceneLoad());
               
            }
            
            
        }
    }
    public void Climbing()
    {
        // float vertical = Input.GetAxis("Vertical");
       
        if (isChain && Mathf.Abs(direction.y) > 0)
        {
            isClimbing = true;
            //print("isClimbing="+ isClimbing);
        }
        if (isClimbing)
        {
            if(direction.y > 0.55f)
            {
                rb2d.gravityScale = 0;
                anim.speed = 1;
                rb2d.velocity = new Vector2(0,direction.y * 5);
                ChangeAnimState(Climb);
                
            }
            if (direction.y == 0)
            {
                rb2d.gravityScale = 0;
                rb2d.velocity = new Vector2(0, direction.y * 0);
                anim.speed = 0;
                ChangeAnimState(Climb);
            }
           

        }
        else
        {
            rb2d.gravityScale = 1;
            anim.speed = 1;
        }

        



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chain"))
        {
            isChain = true;
            //print("Sobe");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chain"))
        {
            isClimbing = false;
            isChain = false;
        }


    }
    IEnumerator SceneLoad() 
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}