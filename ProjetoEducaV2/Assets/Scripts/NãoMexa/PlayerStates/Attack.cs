using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IStates
{
    public float duracaoDocombo;
    public float tempoMax;
    public float tempParaProximoAtaque;
    public int animAtual;
    public AnimationClip[] animCombo;
    public float timeToResetAttack;
    public float timeSinceLastAttack;

    public Attack(Animator animator, Rigidbody2D rb2d, float duracaoDocombo, float tempoMax, float tempParaProximoAtaque, int animAtual, AnimationClip[] animCombo, float timeToResetAttack, float timeSinceLastAttack) : base(animator, rb2d)
    {
        this.duracaoDocombo = duracaoDocombo;
        this.tempoMax = tempoMax;
        this.tempParaProximoAtaque = tempParaProximoAtaque;
        this.animAtual = animAtual;
        this.animCombo = animCombo;
        this.timeToResetAttack = timeToResetAttack;
        this.timeSinceLastAttack = timeSinceLastAttack;
       

    }
    public override void OnBegin(Vector2 direction, bool isMove, bool isAttacking)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            rb2d.velocity = new Vector2(direction.x * 0, rb2d.velocity.y);
            if (Time.time > tempParaProximoAtaque)
            {
                animator.Play(animCombo[animAtual].name);
                tempParaProximoAtaque = Time.time + tempoMax;
                animAtual++;
                if (animAtual >= animCombo.Length)
                {
                    animAtual = 0;
                }
            }
            if (Time.time > tempParaProximoAtaque + duracaoDocombo)
            {
                tempParaProximoAtaque = Time.time;
                animAtual = 0;
            }

            
        }
        
    }
    public override EStates OnUpdate(Vector2 direction, bool isJumpingPressed, bool isGrounded, bool isAttackinPressed)
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
        if (isGrounded && isAttackinPressed)
        {
            nextState = EStates.Attack;
        }
        return nextState;
    }

    public override void OnExit()
    {

    }

   
}
