using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : IStates
{
    CharacterState characterState;
    PlayerController playerController;
    Vector2 direction;
    public Run(PlayerController controller, CharacterState character): base(controller, character)
    {
        characterState = character;
        playerController = controller;
       
    }


    public override void OnBegin()
    {

        animator.Play(Run);
       
    }
    public override EStates OnUpdate()
    {
       
        if (characterState.isGrounded)
        {
            if (Mathf.Abs(characterState.isMovingX) != 0)
            {
                characterState.Flip(playerController);
                nextState = EStates.Run;
            }
            if (Mathf.Abs(characterState.isMovingX) == 0)
            {
                nextState = EStates.Idle;
            }
            if (characterState.isAttackingPressed )
            {
                nextState = EStates.Attack;
            }
           
        }
        if (characterState.isJumpingPressed && characterState.isGrounded || !characterState.isGrounded)
        {
            nextState = EStates.Jump;
        }


        return nextState;
    }
    public override void OnFixedUpdate()
    {
        characterState.isGrounded = Physics2D.OverlapCircle(characterState.groundCheck.position, characterState.groundRadius, characterState.groundLayer);
        rb2d.velocity = new Vector2(characterState.isMovingX * characterState.speed, rb2d.velocity.y);

    }


    public override void OnExit()
    {
        nextState = EStates.Run;
    }

}


