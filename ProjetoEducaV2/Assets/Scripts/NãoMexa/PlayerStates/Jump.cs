using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IStates
{
  
    
    float jumpForce;
    public Jump(Animator animator, Rigidbody2D rb2d, float jumpForce) : base(animator, rb2d) { this.jumpForce = jumpForce; }

    public override void OnBegin(Vector2 direction)
    {
        nextState = EStates.Jump;   
        rb2d.velocity = Vector2.up * jumpForce;
        animator.Play("Jump");
    }
    public override EStates OnUpdate(Vector2 direction, bool isJumpingPressed, bool isGrounded)
    {
        
        if (direction.x != 0)
        {
            nextState = EStates.Run;
        }
        else if (direction.x == 0 )
        {
            nextState = EStates.Idle;
        }
        
        return nextState;
    }

    public override void OnExit()
    {

    }

}
