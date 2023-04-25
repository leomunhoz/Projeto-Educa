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

public abstract class IStates
{
    
   protected Animator animator;
   protected Rigidbody2D rb2d;
   protected SpriteRenderer sprite;
   protected static readonly int Idle = Animator.StringToHash("Idle");
   protected static readonly int Run = Animator.StringToHash("Run");
   protected static readonly int Jump = Animator.StringToHash("Jump");
   protected static readonly int Attack = Animator.StringToHash("Attack 1");
   protected static readonly int WallSliding = Animator.StringToHash("SlideWall");
   protected static readonly int Down = Animator.StringToHash("Down");
   protected static readonly int Death = Animator.StringToHash("Death");
   protected static readonly int Climb = Animator.StringToHash("Climb");



    protected EStates nextState;

    public IStates(PlayerController controller, CharacterState character)
    {
        animator =controller.GetComponentInChildren<Animator>();
        rb2d = controller.GetComponent<Rigidbody2D>();
        sprite = controller.GetComponentInChildren<SpriteRenderer>();
    }

    public abstract void OnBegin();
    public abstract EStates OnUpdate();

    public abstract void OnFixedUpdate();
    public abstract void OnExit();

}





