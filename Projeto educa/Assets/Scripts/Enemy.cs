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
    public float velo = 1f;
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

    [SerializeField]int distAtaque=1;
    [SerializeField] int distPersegue = 1;
    [SerializeField] int distPatrulha = 1;
    bool inHome = true;
    int cont = 0;



    // Start is called before the first frame update
    void Start()
    {
         
        initialPosition= new Vector2(transform.position.x, transform.position.y);
        currentHealth = maxHealth;
        animator=GetComponent<Animator>();
        character=GameObject.Find("Character Knight All Animations_0");
        if (character == null)
            Debug.Log("Characeter n�o encontrado");
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
            if (distancia <= distPersegue)
            {
                inHome = false;
                Hunt();
                
            }
            else if(initialPosition==enemyPos)
            {
                animator.SetTrigger("Idle");
                //print("Descan�ando");
            }
            else if (distancia>distPatrulha)
            {
                Home();
            }
            if(rb2d.velocity.x==0)
                animator.SetTrigger("Idle");
        }
    }
    public void TakeDemage(int damage) 
    {
       
        currentHealth -= damage;
        //direction = (knockBack + enemyPos).normalized;
        //rb2d.velocity = direction;
        //isKnockBack = true;
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
        if (enemyPos.x < initialPosition.x + 5)
        {
            direction.x = 1;
            rb2d.velocity = direction * 1;//
            transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
            animator.SetTrigger("Run");
        }
        else
            inHome = false;
        cont++;
        print(cont);
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
        direction.x = (playerPos.x  - enemyPos.x);//.normalized;//move na dire��o do player
        
        
        if (isGrounded)
        { 
            rb2d.velocity = direction * velo;//
            transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
            animator.SetTrigger("Run");
            //print("Correndo");

            if (distancia <= distAtaque)
            {
                //Debug.Log("Atacar");
                rb2d.velocity = direction * 0;//

                //print("Atacando");
            }
            else
            {
                animator.SetTrigger("Idle");
                //print("Descan�ado Ca�a");
            }

        }
    }
    public void Home()
    {
       
        float home;
        if (!inHome)
        {
            if (isGrounded && initialPosition != enemyPos)
            {
                direction = (initialPosition - enemyPos).normalized;//move na dire��o do player
                rb2d.velocity = direction * velo;//
                home = Vector2.Distance(enemyPos, initialPosition);
                transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
                if (home <= 1)
                {
                    rb2d.velocity = direction * 0;//
                                                  //print("Voltando para casa");
                    animator.SetTrigger("Idle");
                    inHome = true;
                }

            }
        }
        else
            Patrulha();
        
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Ground.position, 0.1f);
    }
}
