using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IStates
{




    public Idle(Animator animator, Rigidbody2D rb2d) : base(animator, rb2d) { }
    
     

    public override void OnBegin(Vector2 direction)
    {
        nextState = EStates.Idle;
        animator.Play("Idle");

    }
       

    public override EStates OnUpdate(Vector2 direction, bool isJumpingPressed, bool isGrounded)
    {
        if (direction.x == 0 && rb2d.velocity.x == 0)
        {
            nextState = EStates.Idle;
        }
        else if (direction.x != 0 )
        {
            nextState = EStates.Run;

        }
        if (isJumpingPressed && isGrounded)
        {
            nextState = EStates.Jump;
        }

        return nextState;
    }

    public override void OnExit()
    {
        //
    }
}
