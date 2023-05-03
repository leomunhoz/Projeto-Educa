using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
/*Cores dos Raios: 
 * Verde: Chão
 * Vermelho: Patrulha
 * Branco: Parede
*/
public class Inimigo : MonoBehaviour
{
    public GameObject player;
    public PlayerOne play;
    public GameObject Projetil;
    public LayerMask walLayer;
    public LayerMask chao;
    public LayerMask PlayerLayer;
    public RaycastHit2D playerHitFrente;
    public RaycastHit2D playerOUParece;
    public RaycastHit2D obstaculo;

    public Vector2 posHero;//Sugeito a GameManager
    public Vector2 posInimigo;
    public Vector2 direcao;
    private Vector2 raioAFrente;
    public Vector2 pontoInicial;
    public Vector2 pontoFinal;
    public Vector2 spearPosition;

    public bool indoParaDireita = true;
    public bool fechadura = false;
    public bool isDead = false;
    public bool emAtaque = false;
    public bool parede = false;


    public float posY;
    public float disPlayerRay;
    public float disParede;
    public float currentHealth;
    public float tempoDeMorte = 20f;
    public float herovsInimigo;
    public float vidaTotal;//virá do construct
    public float disPersegue;//virá do construct
    private float disAtaque;//virá do construct
    public float velocidade;//virá do construct
    public float disPatrulha;//virá do construct
    public int dano;
    public int defesa;
    public string nome;

    private Rigidbody2D rd;

    /* public float radius;
     public GameObject animacaoDanoPrefab;
     public LayerMask playerLayer;*/
    public void Parametros(string nomeC, int danoC, int def, float Persegue, float Ataque, float Patrulha, float velo, float vidaToda)
    {
        disPersegue = Persegue;
        disAtaque = Ataque;
        disPatrulha = Patrulha;
        velocidade = velo;
        vidaTotal = vidaToda;
        defesa = def;
        dano = danoC;
        nome = nomeC;
    }

    private void Start()
    {
        
        //print("Nome=" + nome);
        pontoInicial = transform.position;
        pontoFinal = pontoInicial + Vector2.right * disPatrulha;
        player = GameObject.FindGameObjectWithTag("Player");
        walLayer = LayerMask.GetMask("Chao");
        chao = LayerMask.GetMask("Chao","Platform");
        PlayerLayer = LayerMask.GetMask("Player");
        currentHealth = vidaTotal;
        rd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isDead)
        {
            direcao = indoParaDireita ? Vector2.right : Vector2.left;
            posHero = new Vector2(player.transform.position.x, player.transform.position.y);
            posInimigo = new Vector2(transform.position.x, transform.position.y);
            herovsInimigo = Vector2.Distance(posHero, posInimigo);
            
            //print(posY);

            raioAFrente = transform.TransformPoint(0.5f, 0.0f, 0.0f);
            RaycastHit2D surfaceHit = Physics2D.Raycast(raioAFrente, Vector2.down, 4f, chao);
            Debug.DrawRay(raioAFrente, dir: transform.TransformDirection(Vector2.down) * 1.75f, color: Color.green);

            playerHitFrente = Physics2D.Raycast(transform.position, direcao, disPersegue, PlayerLayer);
            Debug.DrawRay(posInimigo, dir: direcao * disPersegue, color: Color.yellow);
            disPlayerRay = playerHitFrente.distance;

            playerOUParece = Physics2D.Raycast(transform.position, direcao, 1000, walLayer);
            disParede = playerOUParece.distance;

            //posY = (posInimigo.y) - (posHero.y);

            if (disPlayerRay == 0)
                disPlayerRay = disParede + 1f;

            if (surfaceHit.collider == null)
            {
                MudaPatrulha();
                
            }
            /*if ((disParede < disPlayerRay))
                parede = true;
            else
                parede = false;*/
            if (herovsInimigo < disPersegue && !parede)
            {
                if (Mathf.Abs(Mathf.Abs(posInimigo.y) - Mathf.Abs(posHero.y)) < 2)
                {
                    Atacar();
                }
                else
                {
                    Persegue();
                    
                }
            }
            else
            {
                Patrulhar();
                emAtaque = false;
            }

        }
    }
    private void FixedUpdate()
    {
       
        // playerHitCosta = Physics2D.Raycast(transform.position, Vector2.left,disPersegue, PlayerLayer);
    }

    private void Patrulhar()
    {
        // Verifica se há obstáculos no caminho
        obstaculo = Physics2D.Raycast(transform.position, direcao, disPatrulha - (disPatrulha - 1), walLayer);
        Debug.DrawRay(posInimigo, dir: direcao * 2.0f, color: Color.white);

        {
            if (obstaculo.collider != null)
            {
                // Se houver obstáculo, muda a direção
                MudaPatrulha();
                indoParaDireita = !indoParaDireita;

            }
            else
            {
                Move();
            }

            // Verifica se chegou no ponto final ou inicial e muda a direção
            if ((indoParaDireita && transform.position.x > pontoFinal.x) || (!indoParaDireita && transform.position.x < pontoInicial.x))
            {
                indoParaDireita = !indoParaDireita;
                fechadura = false;
            }
        }
    }
    public void Persegue()

    {
        ViraParaPlayer();
        if (herovsInimigo >= disAtaque)
            Move();
        else
            AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Idle");
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha a linha de patrulha
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pontoInicial, pontoFinal);
    }
    public void Move()
    {
        AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Run");
        rd.velocity = direcao * velocidade;
        // Vira o inimigo para a direção do movimento
        transform.localScale = new Vector2(Mathf.Sign(direcao.x), 1f);
    }
    public void MudaPatrulha()
    {
        if (!fechadura)//Em contado com Patrulha()
        {
            if (indoParaDireita)
            {
                //("Direita");
                pontoInicial.x = transform.position.x;
                pontoFinal.x = transform.position.x;
                pontoFinal.x = (transform.position.x - 1f);
                pontoInicial.x = (pontoInicial.x - disPatrulha);
            }
            else
            {
                //("Esquerda");
                pontoInicial.x = transform.position.x;
                pontoFinal.x = transform.position.x;
                pontoInicial.x = (transform.position.x + 1f);
                pontoFinal.x = (pontoFinal.x + disPatrulha);
            }
            indoParaDireita = !indoParaDireita;
            fechadura = true;
        }
    }
    public void TakeDemage(int damage)
    {
        currentHealth = currentHealth - (damage - defesa);
        //AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Hurt");
        StartCoroutine(Flash());
        if (currentHealth <= 0)
        {
            AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Death");
            isDead = true;
            rd.velocity = direcao * 0;
            //currentHealth = 0;
            this.enabled = false;
            rd.gravityScale = 0;
            Destroy(this.gameObject, tempoDeMorte);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Inimigo>().enabled = false;
            play = player.GetComponent<PlayerOne>();
            play.mortos++;
            print(nome + " Mortos "+ play.mortos);
        }
    }
    //
    IEnumerator Flash()
    {
        float flashDuration = 0.1f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // salva a cor original do sprite
        Color originalColor = spriteRenderer.color;

        // define a cor vermelha
        Color damageColor = Color.red;

        // muda a cor do sprite para vermelho
        spriteRenderer.color = damageColor;

        // espera um tempo
        yield return new WaitForSeconds(flashDuration);

        // retorna a cor original do sprite
        spriteRenderer.color = originalColor;

    }
    //
    public void Atacar()
    {
        {
            direcao = ViraParaPlayer();
            if (indoParaDireita)//Direita
            { 
                if (!emAtaque)
                 {
                    
                    //AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Idle");
                    if ((posHero.x > posInimigo.x && Vector2.Dot(transform.right, direcao) > 0))
                      {
                        {
                            emAtaque = true;
                            spearPosition = new Vector2(transform.position.x + 0.5f, transform.position.y - 0.3f);
                            AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
                            rd.velocity = direcao * 0;
                        }
                      }
                 }
             }
             else//Esquerda
             {

                if (!emAtaque )
                 {
                    //AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Idle");
                    if ((posHero.x < posInimigo.x && Vector2.Dot(transform.right, direcao) < 0))
                     {
                        //if (act.isJumping || posY < 0.9)
                        {
                            spearPosition = new Vector2(transform.position.x -1f, transform.position.y - 0.3f);
                            emAtaque = true;
                            AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
                            rd.velocity = direcao * 0;
                        }
                    }
                 }
             }
         }
    }
    /*IEnumerator Atacar()
    {
        {
            direcao = ViraParaPlayer();
            if (indoParaDireita)
            { 
                if (!emAtaque)
                {
                    print("Ataque passou if 2");
                    //AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Idle");
                    if ((posHero.x > posInimigo.x && Vector2.Dot(transform.right, direcao) > 0))
                    {
                        print("Ataque passou if 3");
                        //if (act.isJumping || posY<0.9)
                        {
                            spearPosition = new Vector2(transform.position.x + 0.5f, transform.position.y - 0.3f);
                            emAtaque = true;
                            AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
                            AnimatorStateInfo animState = AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
                            while (animState.IsName("Attack") && animState.normalizedTime < 1.0f)
                            {
                                yield return null;
                                animState = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                            }
                            rd.velocity = direcao * 0;
                        }
                    }
                }
            }
            else//Esquerda
            {
                if (!emAtaque)
                {
                    print("Ataque passou if 2");
                    //AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Idle");
                    if ((posHero.x < posInimigo.x && Vector2.Dot(transform.right, direcao) < 0))
                    {
                        print("Ataque passou if 3");
                        //if (act.isJumping || posY < 0.9)
                        {
                            spearPosition = new Vector2(transform.position.x - 1, transform.position.y - 0.3f);
                            emAtaque = true;
                            AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
                            AnimatorStateInfo animState = AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
                            while (animState.IsName("Attack") && animState.normalizedTime < 1.0f)
                            {
                                yield return null;
                                animState = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                            }
                            rd.velocity = direcao * 0;
                        }
                    }
                }
            }
        }
    }*/


    public void InstanciarLanca() 
    {
        GameObject Lanca = Instantiate(Projetil, spearPosition, Quaternion.identity);
        Spear spear= Lanca.GetComponent<Spear>();
        if (indoParaDireita)
        {
            spear.direction = Vector2.right;
        }
        else
        {
            spear.direction = Vector2.left;
        }
        spear = null;
    }

    public Vector2 ViraParaPlayer()
    {
        if (posHero.x > posInimigo.x)
        {
            indoParaDireita = true;
            direcao = indoParaDireita ? Vector2.right : Vector2.left;
            transform.localScale = new Vector2(Mathf.Sign(direcao.x), 1f);
            emAtaque = false;
        }
        else//Esquerda
        {
            indoParaDireita = false;
            direcao = indoParaDireita ? Vector2.right : Vector2.left;
            transform.localScale = new Vector2(Mathf.Sign(direcao.x), 1f);

            emAtaque = false;
        }
        return direcao;
    }
}










