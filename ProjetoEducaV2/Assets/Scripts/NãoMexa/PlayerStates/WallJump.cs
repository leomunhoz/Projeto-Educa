using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : IStates
{
    CharacterState characterState;
    PlayerController playerController;
    public WallJump(PlayerController controller, CharacterState character) : base(controller, character) 
    {
        characterState = character;
        
    }
    public override void OnBegin()
    {
        if (characterState.isFacingRigth && characterState.wallJumping)
        {
            rb2d.AddForce(new Vector2(-characterState.wallJumpForce.x, characterState.wallJumpForce.y), ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(new Vector2(characterState.wallJumpForce.x, characterState.wallJumpForce.y), ForceMode2D.Impulse);
        }

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
        }
        if (characterState.isJumpingPressed && characterState.isGrounded || !characterState.isGrounded)
        {
           nextState = EStates.Jump;
        }
        if (!characterState.isGrounded && characterState.isWallsliding)
        {
            characterState.isWallsliding = true;
            nextState = EStates.WallSlide;
        }
        if (characterState.isWallsliding && characterState.isJumpingPressed && !characterState.isGrounded)
        {
            characterState.wallJumping = true;
           
            nextState = EStates.WallJump;
        }
        return nextState;
    }
    public override void OnFixedUpdate()
    {
        characterState.isGrounded = Physics2D.OverlapCircle(characterState.groundCheck.position, 0.2f, characterState.groundLayer);
        characterState.isWallsliding = Physics2D.OverlapCircle(characterState.wallChack.position, 0.5f, characterState.wallLayer);
      
    }

    public override void OnExit()
    {

    }


}
