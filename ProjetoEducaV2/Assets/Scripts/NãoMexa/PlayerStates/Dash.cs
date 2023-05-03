using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : IStates
{
    CharacterState characterState;
   public Dash(PlayerController controller, CharacterState character) :base(controller, character)
   {
    characterState = character; 
   }

    public override void OnBegin()
    {
        animator.Play(Dash);
    }
    public override EStates OnUpdate()
    {
        return nextState;
    }
    public override void OnFixedUpdate()
    {
        
    }

    public override void OnExit()
    {
       
    }


}
