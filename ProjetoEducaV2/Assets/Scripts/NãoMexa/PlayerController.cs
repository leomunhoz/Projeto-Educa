using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;





[System.Serializable]
public class PlayerController : MonoBehaviour
{
     public EStates currentState;
     public EStates nextState;


     public Rigidbody2D rb2d;
     public SpriteRenderer SpriteRenderer;
     public Animator animator;
     public float speed;
     public float jumpForce;
     private int pulosExtras = 1;
     private int axPulosExtras = 1;


    public Transform groundCheck;
    public Transform wallChack;
    public Transform attackCheck;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public LayerMask attackLayer;

    public Vector2 direction;

    public bool isGrounded;
    public bool isWallsliding;
    public bool isMove = false;
    public bool isFacingRigth;
    private bool isAttacking;
    private bool isAttackingPressed;
    public bool isJumpingPressed;
    private bool isRollingPressed;
    private bool isSkeyDownPress;
    private bool isMousePress;
    private bool isJumping;
    private bool isWallSliding;
    private bool isDead;


    private int Idle = Animator.StringToHash("Idle");
    private int Run = Animator.StringToHash("Run");
    private int Jump = Animator.StringToHash("Jump");
    private int Attack = Animator.StringToHash ("Attack 1");
    private int WallSliding = Animator.StringToHash ("SlideWall");
    private int Down = Animator.StringToHash("Down");
    private int Death = Animator.StringToHash("Death");
    private int Climb = Animator.StringToHash("Climb");
    List<IStates> states;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        states = new List<IStates>();
        Idle idle = new Idle(animator, rb2d);
        Run run = new Run(animator, rb2d, speed);
        Jump jump = new Jump(animator, rb2d, jumpForce);
        states.Add(idle);
        states.Add(run);
        states.Add(jump);

        currentState = EStates.Idle;
        nextState = EStates.Idle;
    }
    

    // Update is called once per frame
    void Update()
    {
        //direction = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0.0f);
         nextState = states[(int)currentState].OnUpdate(direction, isJumpingPressed, isGrounded);
        if (nextState != currentState)
        {
            states[(int)currentState].OnExit();
            states[(int)nextState].OnBegin(direction,isMove);
            currentState = nextState;
        }
        
       
        isJumpingPressed = Gamepad.current.buttonSouth.isPressed || Keyboard.current.spaceKey.isPressed;
        isAttackingPressed = Gamepad.current.buttonNorth.isPressed || Keyboard.current.fKey.isPressed;
        isRollingPressed = Gamepad.current.buttonEast.isPressed;
        isSkeyDownPress = Keyboard.current.sKey.isPressed;
        

        if (isFacingRigth && rb2d.velocity.x > 0 || !isFacingRigth && rb2d.velocity.x < 0)
        {
            isFacingRigth = !isFacingRigth;
            Vector2 LocalScale = transform.localScale;
            LocalScale.x *= -1;
            transform.localScale = LocalScale;

        }



    }

    private void FixedUpdate()
    {
        states[(int)nextState].OnBegin(direction,isMove);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f ,groundLayer);
        isWallsliding = Physics2D.OverlapCircle(wallChack.position, 0.5f, wallLayer);
    }

    public void move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        if (direction.x != 0)
        {
            isMove = true;
        }
        else
        {
            isMove=false;
        }
    }     
          
           
       
    
    
   
}
