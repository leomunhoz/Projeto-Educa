using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    string currantState;
    public Animator animator;
    public float tempoDeMorte=20f;
    bool isTakingDamege;
    private GameObject character;
    public float velo = 3f;
    private Rigidbody2D rb2d;
    const string Run = "Run";
    const string Die = "Death";
    const string Hit = "Hurt";
    Vector2 enemyPos;
    Vector2 playerPos;
    float distancia;
    Vector2 initialPosition;
    Vector2 direction;
    Vector2 knockBack;
    bool isKnockBack=false;
    bool isDead = false;

    public Transform Ground;
    public LayerMask Grounded;
    bool isGrounded = false;   




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
        isGrounded = Physics2D.OverlapCircle(Ground.position, 0.2f, Grounded);
        if (!isDead)
        {
            enemyPos = new Vector2(transform.position.x, transform.position.y);
            playerPos = new Vector2(character.transform.position.x, character.transform.position.y);
            knockBack = new Vector2(10, 10);
            distancia = Vector2.Distance(enemyPos, playerPos);
            if (distancia <= 5)
            {
                Hunt();
                
            }
            else
            {
                Home();
            }
        }
    }
    public void TakeDemage(int damage) 
    {
       
        currentHealth -= damage;
        direction = (knockBack + enemyPos).normalized;
        rb2d.velocity = direction;
        isKnockBack = true;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            animator.SetBool("Die", true);
            currentHealth = 0;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            Destroy(this.gameObject, tempoDeMorte);
            GetComponent<Enemy>().enabled = false;
            rb2d.velocity = direction * 0;
            isDead= true;

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
        direction.x = (playerPos.x  - enemyPos.x);//.normalized;//move na direção do player
        //animator.SetTrigger("Run");
        if (isGrounded)
        { 
            rb2d.velocity = direction * velo;//
            transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
            animator.SetTrigger("Run");

            if (distancia <= 1)
            {
                //Debug.Log("Atacar");
                rb2d.velocity = direction * 0;//
            }
        }
        else
            animator.SetTrigger("Idle");
    }
    public void Home()
    {
        float home;
        if (isGrounded && initialPosition != enemyPos)
        { 
            direction = (initialPosition - enemyPos).normalized;//move na direção do player
            rb2d.velocity = direction * velo;//
            home = Vector2.Distance(enemyPos, initialPosition);
            transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
            if (home <= 1)
            {
                rb2d.velocity = direction * 0;//
            }
        }
        else
            animator.SetTrigger("Idle");
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Ground.position, 0.1f);
    }
}
