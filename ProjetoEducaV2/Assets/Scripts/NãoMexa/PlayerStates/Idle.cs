using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IStates
{




    public Idle(Animator animator, Rigidbody2D rb2d) : base(animator, rb2d) { }
    
     

    public override void OnBegin(Vector2 direction, bool isMove,bool isAttacking)
    {
        nextState = EStates.Idle;
        if (!isMove)
        {
            animator.Play("Idle");
            rb2d.velocity = new Vector2(direction.x * 0, rb2d.velocity.y);
        }
       
    }
       

    public override EStates OnUpdate(Vector2 direction, bool isJumpingPressed, bool isGrounded, bool isAttackinPressed)
    {
        if (direction.x == 0 )
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
        if (isGrounded && isAttackinPressed)
        {
            nextState = EStates.Attack;
        }

        return nextState;
    }

    public override void OnExit()
    {
        //
    }
}
