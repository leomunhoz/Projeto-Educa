using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IStates
{
    
   
    Rigidbody2D rb2d;
    Animator animator;


    public Idle(Animator animator, Rigidbody2D rb2d) : base(animator, rb2d) { }
    
     

    public override void OnBegin(Vector2 direction)
    {
        animator.Play("Idle");


    }
       

    public override EStates OnUpdate(Vector2 direction, bool isJumpingPressed, bool isGrounded)
    {
        if (direction.x == 0)
        {
            nextState = EStates.Idle;
        }
        else if (direction.x != 0)
        {
            nextState = EStates.Run;

        }
        if (isJumpingPressed && isGrounded)
        {
            nextState = EStates.Jump;
        }

        return EStates.Idle;
    }

    public override void OnExit()
    {
        //
    }
}
