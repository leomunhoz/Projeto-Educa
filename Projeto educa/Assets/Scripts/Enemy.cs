using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    string currantState;
    public Animator animator;
    public float tempoDeMorte;
    int currentHealth;
    bool isTakingDamege;
    private GameObject character;
    public float velo = 3f;
    private Rigidbody2D rb2d;
    const string Walk = "Walk";
    const string Die = "Death";
    const string Hit = "Hurt";
    Vector2 enemyPos;
    Vector2 playerPos;
    float distancia;
    Vector2 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        
        initialPosition= new Vector2(transform.position.x, transform.position.y);
        currentHealth = maxHealth;
        animator=GetComponent<Animator>();
        character=GameObject.Find("Character Knight All Animations_0");
        if (character == null)
            Debug.Log("Characeter não encontrado");
        rb2d = GetComponent<Rigidbody2D>();

        
    }
    private void Update()
    {
        enemyPos = new Vector2(transform.position.x, transform.position.y);
        playerPos = new Vector2(character.transform.position.x, character.transform.position.y);
        distancia = Vector2.Distance(enemyPos, playerPos);
        if (distancia <= 5)
        {
            Hunt();
;
        }
        else
        {
            Home();
        }

    }
    public void TakeDemage(int damage) 
    {
       
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            animator.SetBool("Die", true);
            currentHealth = 0;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            Destroy(this.gameObject, tempoDeMorte);
            GetComponent<Enemy>().enabled = false;

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
    public void Hunt()
    {
        Vector2 direction = (playerPos - enemyPos).normalized;//move na direção do player
        rb2d.velocity = direction * velo;//

        transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
        if (distancia < 1)
        {
            //Debug.Log("Atacar");
            rb2d.velocity = direction * 0;//
        }
    }
    public void Home()
    {
        float home = 100;
        Vector2 direction = (initialPosition - enemyPos).normalized;//move na direção do player
        rb2d.velocity = direction * velo;//
        home= Vector2.Distance(enemyPos, initialPosition);
        transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
        if (home <= 1)
        {
            rb2d.velocity = direction * 0;//
        }
    }
}
