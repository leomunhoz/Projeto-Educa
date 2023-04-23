using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStates
{
    Idle,
    Run,
    Jump,
   
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

    public abstract void OnBegin(Vector2 direction);
    public abstract EStates OnUpdate(Vector2 direction, bool isJumpingPressed, bool isGrounded);
    public abstract void OnExit();

}





