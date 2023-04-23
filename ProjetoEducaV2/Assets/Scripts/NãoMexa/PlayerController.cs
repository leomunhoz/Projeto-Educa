using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;





[System.Serializable]
public class PlayerController : MonoBehaviour
{
     private EStates currentState;
     private EStates nextState;


     public Rigidbody2D rb2d = null;
     public SpriteRenderer SpriteRenderer = null;
     public Animator animator = null;
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
        currentState = EStates.Idle;
        nextState = EStates.Idle;
        List<IStates> states = new List<IStates>();
        Idle idle = new Idle(animator, rb2d);
        Run run = new Run(animator, rb2d, speed);
        Jump jump = new Jump(animator, rb2d, jumpForce);
        states.Add(idle);
        states.Add(run);
        states.Add(jump);

    }
    

    // Update is called once per frame
    void Update()
    {
         nextState = states[(int)currentState].OnUpdate(direction, isJumpingPressed, isGrounded);
        if (nextState != currentState)
        {
            states[(int)currentState].OnExit();
            states[(int)nextState].OnBegin(direction);
            currentState = nextState;
        }
        //direction = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0.0f);
        isJumpingPressed = Input.GetKeyDown(KeyCode.Escape);
       
        isJumpingPressed = Gamepad.current.buttonSouth.isPressed || Keyboard.current.spaceKey.isPressed;
        isAttackingPressed = Gamepad.current.buttonNorth.isPressed || Keyboard.current.fKey.isPressed;
        isRollingPressed = Gamepad.current.buttonEast.isPressed;
        isSkeyDownPress = Keyboard.current.sKey.isPressed;

        if (isFacingRigth && rb2d.velocity.x < 0 || !isFacingRigth && rb2d.velocity.x > 0)
        {
            isFacingRigth = !isFacingRigth;
            Vector2 LocalScale = transform.localScale;
            LocalScale.x *= -1;
            transform.localScale = LocalScale;

        }



    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f ,groundLayer);
        isWallsliding = Physics2D.OverlapCircle(wallChack.position, 0.5f, wallLayer);
        states[(int)nextState].OnBegin(direction);
    }

  public void move(InputAction.CallbackContext context) 
    {
        direction = context.ReadValue<Vector2>();
    }
    
   
}
