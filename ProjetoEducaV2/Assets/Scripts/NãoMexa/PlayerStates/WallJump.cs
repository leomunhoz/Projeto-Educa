using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : IStates
{
    CharacterState characterState;
    public WallJump(PlayerController controller, CharacterState character) : base(controller, character) 
    {
        characterState = character;
    }
    public override void OnBegin()
    {
      
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
