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
       
    }

    private void FixedUpdate()
    {
        characterState.OnFixedUpdate();
    }
}
