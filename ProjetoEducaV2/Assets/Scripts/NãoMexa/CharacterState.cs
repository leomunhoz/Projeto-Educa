using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class CharacterState
{
   
    //List<IStates> states;
    private IStates[] states;
    public float isMovingX;
    public float IsMovingY;
    public float speed;
    public float jumpForce;
    public int pulosExtras = 1;
    public int axPulosExtras = 1;
    public float WallSlidingSpeed = 2f;
    public float timeBetweenHits = 0.5f;
    public int maxComboHits = 4;
    public string[] comboAnimations;
    public int currentComboHits = 0;
    public float timeSinceLastHit = 0f;
    public float timeAttackingStarted;


    public Transform groundCheck;
    public Transform wallChack;
    public Transform attackCheck;
    public int attackRange;
    public int attackDamage;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public LayerMask attackLayer;
    public LayerMask EnemyLayer;

    public bool isGrounded;
    public bool isWallsliding;

    public bool isFacingRigth;
    public bool isAttacking;
    public bool isAttackingPressed;
    public bool isJumpingPressed;
    public bool isRollingPressed;
    public bool isSkeyDownPress;
    public bool isMousePress;
    public bool isJumping;
    public bool isWallSliding;
    public bool isDead;

    [SerializeField]
    private EStates currentState = EStates.Idle;

    public void OnBegin(PlayerController controller)
    {
        states = new IStates[] { new Idle(controller, this), new Run(controller, this), new Jump(controller, this), new Attack(controller, this),new WallSlide(controller,this) };
        states[0].OnBegin();
    }

    public void OnUpdate()
    {
        EStates nextState = states[(int)currentState].OnUpdate();
        if (nextState != currentState)
            ChangeState(nextState);

        isJumpingPressed = Gamepad.current.buttonSouth.isPressed || Keyboard.current.spaceKey.isPressed;
        isAttackingPressed = Gamepad.current.buttonNorth.isPressed || Keyboard.current.fKey.isPressed;
        isRollingPressed = Gamepad.current.buttonEast.isPressed || Keyboard.current.cKey.isPressed;
        isSkeyDownPress = Keyboard.current.sKey.isPressed;
        isMovingX = Gamepad.current.leftStick.x.ReadValue() + (Keyboard.current.dKey.isPressed ? 1 : 0) + (Keyboard.current.aKey.isPressed ? -1 : 0);
        IsMovingY = Gamepad.current.leftStick.y.ReadValue() + +(Keyboard.current.wKey.isPressed ? 0 : 1) + (Keyboard.current.sKey.isPressed ? 0 : -1);

       


    }
    public void OnFixedUpdate()
    {
        states[(int)currentState].OnFixedUpdate();

       
    }
    private void ChangeState(EStates nextState)
    {
        states[(int)currentState].OnExit();
        states[(int)nextState].OnBegin();
        currentState = nextState;
    }
}



   

  



