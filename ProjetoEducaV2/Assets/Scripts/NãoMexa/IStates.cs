using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStates
{
    Idle,
    Run,
    Jump,
    Attack
   
}
[System.Serializable]
public abstract class IStates
{
   protected Animator animator;
   protected Rigidbody2D rb2d;
   
   

    protected EStates nextState;

    public IStates(Animator animator, Rigidbody2D rb2d)
    {
        this.animator = animator;
        this.rb2d = rb2d;
    }

    public abstract void OnBegin(Vector2 direction, bool isMove, bool isAttacking);
    public abstract EStates OnUpdate(Vector2 direction, bool isJumpingPressed, bool isGrounded, bool isAttackinPressed);
    public abstract void OnExit();

}





