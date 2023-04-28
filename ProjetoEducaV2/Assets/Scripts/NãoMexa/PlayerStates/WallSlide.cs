using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : IStates
{
    public CharacterState characterState;
    public WallSlide(PlayerController controller,CharacterState character): base(controller, character) 
    {
        characterState = character;
    }
    public override void OnBegin()
    {
        animator.Play(WallSliding);
        
    }

    public override EStates OnUpdate()
    {
        if (characterState.isGrounded )
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
        if (characterState.isMovingX != 0)
        {
            rb2d.velocity = new Vector2(characterState.isMovingX * characterState.speed, characterState.IsMovingY);
           
        }
        else
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -characterState.WallSlidingSpeed, float.MaxValue));
            
        }
       

    }
    public override void OnExit()
    {
        nextState = EStates.WallSlide;
    }

}
