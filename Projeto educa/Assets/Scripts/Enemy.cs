using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    string currantState;
    public Animator animator;
    int currentHealth;
    bool isTakingDamege;

    const string Walk = "Walk";
    const string Die = "Death";
    const string Hit = "Hurt";


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator.GetComponent<Animator>();
    }

   public void TakeDemage(int damage) 
    {
       
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            animator.SetBool("Die", true);
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            Destroy(this.gameObject, 0.7f);
        }
    }

    public void Patrulha() 
    {
        
    
    }

    public void Attack() 
    {
    
    }

    void ChangeAnimState(string newState)
    {
        if (currantState == newState)
        {
            return;
        }
        animator.Play(newState);
    }
}
