using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : IStates
{
    
    Rigidbody2D rb2d;
    Animator animator;
    float speed;

    public Run(Animator animator, Rigidbody2D rb2d,float speed) : base(animator, rb2d) { this.speed = speed; }


    public override void OnBegin(Vector2 direction)
    {
        
        animator.Play("Run");
        rb2d.velocity = new Vector2(direction.x * speed, rb2d.velocity.y);
    }
    public override EStates OnUpdate(Vector2 direction, bool isJumpingPressed, bool isGrounded)
    {
        if (direction.x != 0) 
        {
            nextState = EStates.Run;
        }
        else if (direction.x == 0)
        {
            nextState = EStates.Idle;
        }
        if (isJumpingPressed && isJumpingPressed)
        {
            nextState = EStates.Jump;
        }
        return EStates.Run;
    }
    public override void OnExit()
    {
      
    }
}


