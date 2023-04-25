using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class CharacterState 
{
    //List<IStates> states;
    private IStates[] states;
    public float speed;
    public float jumpForce;
    private int pulosExtras = 1;
    private int axPulosExtras = 1;


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


    public float duracaoDocombo = 1f;
    public float tempoMax;
    public float tempParaProximoAtaque;
    public int animAtual = 0;
    public AnimationClip[] Animcombo;
    public float timeToResetAttack = 0.3f;
    public float timeSinceLastAttack = 0f;

    [SerializeField]
    private EStates currentState = EStates.Idle;

    public void OnBegin(PlayerController controller)
    {
        states = new IStates[]{ new Idle(controller, this), new Run(controller, this), new Jump(controller, this), new Attack(controller, this) };
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
