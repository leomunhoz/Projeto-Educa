using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;





public class PlayerController : MonoBehaviour
{
    public CharacterState characterState;

    private void Awake()
    {
     
        characterState.OnBegin(this);
       
    }


    // Update is called once per frame
    void Update()
    {

        characterState.OnUpdate();
        if (characterState.isFacingRigth && characterState.isMovingX > 0 || !characterState.isFacingRigth && characterState.isMovingX < 0)
        {
            characterState.isFacingRigth = !characterState.isFacingRigth;
            Vector3 LocalScale = transform.localScale;
            LocalScale.x *= -1;
            transform.localScale = LocalScale;

        }
    }
        private void FixedUpdate()
    {
        characterState.OnFixedUpdate();
    }
}
