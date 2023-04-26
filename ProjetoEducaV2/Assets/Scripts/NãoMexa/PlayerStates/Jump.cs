using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IStates
{
   
    CharacterState characterState;
   
  
 public Jump(PlayerController controller, CharacterState character) : base(controller, character) 
 {
        
        characterState = character;
 }

    public override void OnBegin()
    {
       
        animator.Play(Jump);
        rb2d.AddForce(new Vector2(0.0f, characterState.jumpForce), ForceMode2D.Impulse);
       
    }
    public override EStates OnUpdate()
    {
        if (characterState.isGrounded)
        {
            if (Mathf.Abs(characterState.isMovingX) != 0)
            {
                nextState = EStates.Run;
            }
            if (Mathf.Abs(characterState.isMovingX) == 0)
            {
                nextState = EStates.Idle;
            }
            if (characterState.isJumpingPressed)
            {
                nextState = EStates.Jump;
            }
        }
        if (!characterState.isGrounded && characterState.isWallsliding)
        {
            characterState.isWallsliding = true;
            nextState = EStates.WallSlide;
        }
        return nextState;

    }
    public override void OnFixedUpdate()
    {
        characterState.isGrounded = Physics2D.OverlapCircle(characterState.groundCheck.position, characterState.groundRadius, characterState.groundLayer);
        characterState.isWallsliding = Physics2D.OverlapCircle(characterState.wallChack.position, characterState.WallRadius, characterState.wallLayer);
        
    }

    public override void OnExit()
    {
        nextState = EStates.Jump;
    }

}
