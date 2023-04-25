using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IStates
{
    
    
    CharacterState characterState;
 public Idle(PlayerController controller, CharacterState character) : base(controller,character) 
    {
       
       
        characterState = character;
       
    }
    
     

    public override void OnBegin()
    {
        
     animator.Play(Idle);
     rb2d.velocity = new Vector2(characterState.isMovingX * 0, rb2d.velocity.y);
    }
       

    public override EStates OnUpdate()
    {
        if (characterState.isGrounded)
        {
            if (characterState.isMovingX == 0)
            {
                nextState = EStates.Idle;
            }
            if (characterState.isMovingX != 0)
            {
                nextState = EStates.Run;

            }
            if (characterState.isJumpingPressed)
            {
                nextState = EStates.Jump;
            }
            if (characterState.isAttackingPressed)
            {
                nextState = EStates.Attack;
            }
        }
      

        return nextState;
    }
    public override void OnFixedUpdate()
    {
        characterState.isGrounded = Physics2D.OverlapCircle(characterState.groundCheck.position, 0.2f, characterState.groundLayer);
       
    }

    public override void OnExit()
    {
        nextState = EStates.Idle;
    }

}
