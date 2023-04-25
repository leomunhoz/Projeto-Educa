using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IStates
{
   
    CharacterState characterState;
    Vector2 direction;
  
 public Jump(PlayerController controller, CharacterState character) : base(controller, character) 
 {
        direction = controller.direction;
        characterState = character;
 }

    public override void OnBegin()
    {
       
        animator.Play(Jump);
        rb2d.velocity = Vector2.up * characterState.jumpForce;

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


            if (characterState.isJumpingPressed)
            {
                nextState = EStates.Jump;
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
        nextState = EStates.Jump;
    }

}
