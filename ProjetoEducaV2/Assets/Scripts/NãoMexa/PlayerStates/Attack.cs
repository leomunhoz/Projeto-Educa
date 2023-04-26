using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IStates
{
    CharacterState characterState;
    
   
    public Attack(PlayerController controller, CharacterState character) : base(controller, character)
    {

      characterState = character;

    }
    public override void OnBegin()
    {
        rb2d.velocity = Vector2.zero;
        if (characterState.timeSinceLastHit < characterState.timeBetweenHits && characterState.currentComboHits < characterState.maxComboHits)
        {
            characterState.currentComboHits++;
        }
        else
        {
            characterState.currentComboHits = 1;
        }
        int index = Mathf.Clamp(characterState.currentComboHits - 1, 0, characterState.comboAnimations.Length - 1);
        animator.Play(characterState.comboAnimations[index]);

        characterState.timeSinceLastHit = 0f;
        characterState.timeSinceLastHit += Time.deltaTime;

        if (characterState.timeSinceLastHit > characterState.timeBetweenHits)
        {
            characterState.currentComboHits = 0;
        }

        if (characterState.currentComboHits >= characterState.maxComboHits)
        {
            animator.Play(characterState.comboAnimations[index]);
            characterState.currentComboHits = 0;
            characterState.timeSinceLastHit = 0f;
        }

        Collider2D[] EnemyHits = Physics2D.OverlapCircleAll(characterState.attackCheck.position, characterState.attackRange, characterState.EnemyLayer);
        foreach (var enemy in EnemyHits)
        {
            //Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Inimigo>().TakeDemage(characterState.attackDamage);
        }

    }
       

    public override EStates OnUpdate()
    {
      if (characterState.isGrounded)
       {

        if (Mathf.Abs(characterState.isMovingX) != 0)
        {
            
            nextState = EStates.Run;
        }
        if (Mathf.Abs(characterState.isMovingX) == 0)
        {
            nextState = EStates.Idle;
        }
        if (characterState.isAttackingPressed )
        {
                characterState.isAttacking = true;
                nextState = EStates.Attack;
        }

      }
       

        return nextState;
    }
    public override void OnFixedUpdate()
    {
        characterState.isGrounded = Physics2D.OverlapCircle(characterState.groundCheck.position, characterState.groundRadius, characterState.groundLayer);
       

    }

    public override void OnExit()
    {
        nextState = EStates.Attack;
    }

   }
/*if (characterState.timeSinceLastHit < characterState.timeBetweenHits && characterState.currentComboHits < characterState.maxComboHits)
{
    characterState.currentComboHits++;
}
else
{
    characterState.currentComboHits = 1;
}
int index = Mathf.Clamp(characterState.currentComboHits - 1, 0, characterState.comboAnimations.Length - 1);
animator.Play(characterState.comboAnimations[index]);
characterState.timeSinceLastHit = 0f;
characterState.timeSinceLastHit += Time.deltaTime;

if (characterState.timeSinceLastHit > characterState.timeBetweenHits)
{
    characterState.currentComboHits = 0;
}

if (characterState.currentComboHits >= characterState.maxComboHits)
{
    animator.Play(characterState.comboAnimations[index]);
    characterState.currentComboHits = 0;
    characterState.timeSinceLastHit = 0f;
}

Collider2D[] EnemyHits = Physics2D.OverlapCircleAll(characterState.attackCheck.position, characterState.attackRange, characterState.EnemyLayer);
foreach (var enemy in EnemyHits)
{
    //Debug.Log("Hit" + enemy.name);
    enemy.GetComponent<Inimigo>().TakeDemage(characterState.attackDamage);
}*/