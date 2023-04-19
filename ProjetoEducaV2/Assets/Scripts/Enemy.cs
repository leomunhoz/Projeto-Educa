using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{/*
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
    float home;



    // Start is called before the first frame update
    void Start()
    {
         
        initialPosition= new Vector2(transform.position.x+0.1f, transform.position.y);
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
            print("inHome update=" + inHome);
            if (distancia <= distPersegue)
            {
                inHome = false;
                //Hunt();
                
            }
            if (distancia>=distPatrulha)
            {
                //print("Distancia: " + distancia+" Entrando em Home");
                //Home();
            }
            if (distancia <= distAtaque)
            {
                inHome = false;
                rb2d.velocity = direction * 0;//
                print("Atacando");
            }
            if (rb2d.velocity.x==0 && rb2d.velocity.y == 0)
                animator.SetTrigger("Idle");
        }
    }*/
    /*   public void TakeDemage(int damage) 
       {

           currentHealth -= damage;
           animator.SetTrigger("Hurt");

           if (currentHealth <= 0)
           {
               animator.SetBool("Die", true);
               isDead = true;
               rb2d.velocity = direction * 0;
               currentHealth = 0;
               this.enabled = false;
               Destroy(this.gameObject, tempoDeMorte);
               GetComponent<Collider2D>().enabled = false;
               GetComponent<Enemy>().enabled = false;
           }
       }

       public void Patrulha()
       {
           /*    if (enemyPos.x <= initialPosition.x + 5f && enemyPos.y <= initialPosition.y)
                {
                    direction.x = 1;
                    rb2d.velocity = direction * 1;//* Time.deltaTime;
                    transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
                    animator.SetTrigger("Run");
                    print("patrol");
                    //print("Posição do inimigo: "+enemyPos+ "initialPosition: "+ initialPosition.x+5);
                }
                if(enemyPos.x >= initialPosition.x + 3f)
                {
                    inHome = false;
                    print("Volta");
                }
                //cont++;
                //print("Volta: "+cont);
           */
    /*   }
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
     /*  public void Hunt()
       {
           direction.x = (playerPos.x  - enemyPos.x);//.normalized;//move na direção do player


           if (isGrounded)
           { 
               rb2d.velocity = direction * velo;//
               transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
               animator.SetTrigger("Run");
               //print("Correndo");
           }
       }*/
    /*   public void Home()
       {
              if (!inHome)
              {
                  print("inHome dentro de Home=" + inHome+"Initial position"+initialPosition);
                  if (isGrounded && initialPosition != enemyPos)
                  {
                      direction = (initialPosition - enemyPos).normalized;//move na direção do player
                      rb2d.velocity = direction * 1;//
                      home = Vector2.Distance(enemyPos, initialPosition);
                      transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
                      print("Variavel: home"+home);
                      if (home <= 0.9)
                      {
                          //rb2d.velocity = direction * 0;//
                          print("Chegou "+home);
                          //animator.SetTrigger("Idle");
                          inHome = true;
                          print("inHome Home="+ inHome);
                      }

                  }
              }
              else
                  Patrulha();

       }*/



    /*public class NPC : MonoBehaviour
    {*/
    public GameObject player;
    public float velocidade = 2.0f;
    public float distanciaMinima = 5.0f;
    public LayerMask layerMask;
    public Vector2 posicaoInicial;
    private Vector2 raioAFrente;
    public float distanciaPvsE;
    public Transform Ground;
    public LayerMask grounded;
    [SerializeField] public bool isGrounded = false;
    public Vector2 direcao;
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    public float tempoDeMorte = 20f;
    bool isDead = false;
    private Vector2 enemyPos;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hero");
        posicaoInicial = new Vector3 (transform.position.x, transform.position.y,0f);
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        print("Inicio, posiçãoinicial: " + posicaoInicial);
        layerMask = LayerMask.GetMask("Wall");
        grounded = LayerMask.GetMask("chao");
        Ground = GameObject.Find("Ground").GetComponent<Transform>();
    }

    void Update()
    {
        if (!isDead)
        { 
            enemyPos= new Vector3(transform.position.x, transform.position.y, 0f);

            RaycastHit2D surfaceHit = Physics2D.Raycast(enemyPos, Vector2.down, 5f, grounded);
            surfaceHit = Physics2D.Raycast(Ground.position, Vector2.down, 5f, grounded);// Ground.position, 0.2f, Grounded
            raioAFrente = transform.TransformPoint(0.0f, 0.0f, 0.0f);
            Debug.DrawRay(raioAFrente, dir: transform.TransformDirection(Vector2.down) * 3.0f, color: Color.blue);
            if (surfaceHit.collider != null)
            {
                // Move o NPC para cima da superfície detectada
                //enemyPos = new Vector2(transform.position.x, surfaceHit.point.y + 0.2f);
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            if (isGrounded)
            {

                direcao.x = (player.transform.position.x - transform.position.x);//.normalized;
                direcao.y = (player.transform.position.y - transform.position.y);//.normalized;
                direcao.y = 0f;
                RaycastHit2D hit;
                Debug.DrawRay(enemyPos, dir: direcao * 1.0f, color: Color.red);
                hit = Physics2D.Raycast(transform.position, direcao, distanciaMinima, layerMask);
                if (hit.collider != null)
                {
                    // Se o Raycast atingir um objeto que não seja o player, para o NPC
                    if (hit.collider.gameObject != player && hit.collider.gameObject != gameObject)
                    {
                        print("Parede.");
                        return;
                    }
                }

                distanciaPvsE = Vector2.Distance(enemyPos, player.transform.position);

                /*if (player.GetComponent<Hero>().isInvincible)
                {
                    // Se o player estiver invencível, foge
                    transform.Translate(-direcao * velocidade * Time.deltaTime, Space.World);
                    transform.right = -direcao;
                    print("Foge");
                }
                else*/
                if (distanciaPvsE < distanciaMinima)
                {
                    Perseguir();
                }
                else
                {

                    if (Vector2.Distance(enemyPos, posicaoInicial) > 0.1f)
                    {
                        Volta();
                    }
                    else
                    {
                        Patrulha();
                    }
                }
            }
        }
    }



    public void Attack()
    {

    }
    public void Patrulha()
    {
        // Se o NPC já estiver perto da posição inicial, gera uma nova posição aleatória
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        posicaoInicial.x = (Random.Range(2.0f, posicaoInicial.x));
        //posicaoInicial.x = (Random.Range(-2.0f, posicaoInicial.x));
    }
    public void Perseguir()
    {
        // Se o player estiver na distância mínima, mova-se em direção a ele
        //transform.localScale = new Vector2(Mathf.Sign(direcao.x), 1);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        transform.Translate(direcao * velocidade * Time.deltaTime, Space.Self);
        transform.right = direcao;
        print("Persegue: "+direcao);
    }
    public void Volta()
    {
        // Se o player estiver dentro da distância mínima, retorna à posição inicial
        transform.position = new Vector3(transform.position.x,transform.position.y,0f);
        print("z0"+transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, posicaoInicial, velocidade * Time.deltaTime);
        print("z1" + transform.position.z);
        transform.right = (posicaoInicial - (Vector2)transform.position).normalized;
        print("z1" + transform.position.z);
        print("Volta, posiçãoinicial: " + posicaoInicial);
    }
    public void TakeDemage(int damage)
    {

        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            animator.SetBool("Die", true);
            isDead = true;
            //rb2d.velocity = direcao * 0;
            currentHealth = 0;
            this.enabled = false;
            Destroy(this.gameObject, tempoDeMorte);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Enemy>().enabled = false;
        }
    }
}

