using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : IStates
{
    CharacterState characterState;
    
    Vector2 direction;
    public Run(PlayerController controller, CharacterState character): base(controller, character)
    {
        characterState = character;
        
        direction = controller.direction;
    }


    public override void OnBegin()
    {

        animator.Play(Run);
        rb2d.velocity = new Vector2(direction.x * characterState.speed, rb2d.velocity.y);

    }
    public override EStates OnUpdate()
    {
       
        if (characterState.isGrounded)
        {
            if (direction.x != 0)
            {
               
                nextState = EStates.Run;
            }
            if (direction.x == 0)
            {
                nextState = EStates.Idle;
            }
            if (characterState.isAttackingPressed)
            {
                nextState = EStates.Attack;
            }
        }
        
        if (characterState.isJumpingPressed && characterState.isGrounded)
        {
            nextState = EStates.Jump;
        }
       
        return nextState;
    }
    public override void OnFixedUpdate()
    {
        characterState.isGrounded = Physics2D.OverlapCircle(characterState.groundCheck.position, 0.2f, characterState.groundLayer);
        
    }
      
      
    public override void OnExit()
    {
        nextState = EStates.Run;
    }

}


