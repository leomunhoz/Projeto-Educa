using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IStates
{
    CharacterState characterState;
    Vector2 direction;
    
   
    public Attack(PlayerController controller, CharacterState character) : base(controller, character)
    {

        direction = controller.direction;
        characterState = character;

    }
    public override void OnBegin()
    {
        if (!characterState.isAttacking)
        {
            characterState.isAttacking = true;
            rb2d.velocity = new Vector2(direction.x  , rb2d.velocity.y);
            if (Time.time > characterState.tempParaProximoAtaque)
            {
                animator.Play(characterState.Animcombo[characterState.animAtual].name);
                characterState.tempParaProximoAtaque = Time.time + characterState.tempoMax;
                characterState.animAtual++;
                if (characterState.animAtual >= characterState.Animcombo.Length)
                {
                    characterState.animAtual = 0;
                }
            }
            if (Time.time > characterState.tempParaProximoAtaque + characterState.duracaoDocombo)
            {
                characterState.tempParaProximoAtaque = Time.time;
                characterState.animAtual = 0;
            }


            Collider2D[] EnemyHits = Physics2D.OverlapCircleAll(characterState.attackCheck.position,characterState.attackRange, characterState.EnemyLayer);
            foreach (var enemy in EnemyHits)
            {
                //Debug.Log("Hit" + enemy.name);
                enemy.GetComponent<Inimigo>().TakeDemage(characterState.attackDamage);
            }

            characterState.isAttacking = false;
        }
        
    }
    public override EStates OnUpdate()
    {
      if (characterState.isGrounded)
       {

        if (direction.x == 0)
        {
            nextState = EStates.Idle;
        }
        if (direction.x != 0)
        {
            nextState = EStates.Run;
        }
        if (characterState.isAttackingPressed)
        {
            nextState = EStates.Attack;
        }

       }
     
        return nextState;
    }
    public override void OnFixedUpdate()
    {
        characterState.isGrounded = Physics2D.OverlapCircle(characterState.groundCheck.position, 0.2f, characterState.groundLayer);
    
    }

    public override void OnExit()
    {
        nextState = EStates.Attack;
    }

   
}
