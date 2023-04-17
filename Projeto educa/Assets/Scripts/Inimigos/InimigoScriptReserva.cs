using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
/*Cores dos Raios: 
 * Verde: Chão
 * Vermelho: Patrulha
 * Branco: Parede
*/
/*public class Inimigo : MonoBehaviour
{
    public GameObject player;
    public LayerMask walLayer;
    public LayerMask chao;

    public Vector2 posHero;//Sugeito a GameManager
    public Vector2 posInimigo;
    public Vector2 direcao;
    private Vector2 raioAFrente;
    public Vector2 pontoInicial;
    public Vector2 pontoFinal;

    private bool indoParaDireita = true;
    public bool fechadura = false;
    bool isDead = false;

    //private int moveX = Animator.StringToHash("inputX");
    //private int moveY = Animator.StringToHash("inputY");
    public int maxHealth = 100;//virá do construct
    public int currentHealth;
    public float tempoDeMorte = 4f;
    public float herovsInimigo;
<<<<<<< HEAD:Projeto educa/Assets/Scripts/Inimigos/InimigoScriptReserva.cs
    private float persegue;//virá do construct
    private float disAtaque;//virá do construct
    public float velocidade = 2f;//virá do construct
    public float disPatrulha = 5f;//virá do construct

    private Rigidbody2D rd;
    public static void Parametros(float disPercegue,float disAtaque, float disPatrulha, float velocidade, float vidaTotal)
    {
    }

=======
    public float persegue;
    public float ataque;
    public bool fechadura=false;
    //
    #region Const Anim
    readonly int Idle = Animator.StringToHash("Idle");
    readonly int Run = Animator.StringToHash("Run");
    readonly int Jump = Animator.StringToHash("Jump");
    readonly int Attack = Animator.StringToHash("Attack");
    #endregion
    //
    private int currantState;
>>>>>>> 314472e8e0aa23f17ece3165ab80b356645b4262:Projeto educa/Assets/Scripts/Inimigo.cs
    private void Start()
    {
        pontoInicial = transform.position;
        pontoFinal = pontoInicial + Vector2.right * disPatrulha;
        player = GameObject.FindGameObjectWithTag("Player");
        persegue = 10f;
        walLayer =  LayerMask.GetMask("Wall");
        chao = LayerMask.GetMask("chao");
        disAtaque = 3f;
        currentHealth = maxHealth;
        rd=GetComponent<Rigidbody2D>();
       //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDead)
        {
            direcao = indoParaDireita ? Vector2.right : Vector2.left;
            posHero = new Vector2(player.transform.position.x, player.transform.position.y);
            posInimigo = new Vector2(transform.position.x, transform.position.y);
            herovsInimigo = Vector2.Distance(posHero, posInimigo);
            raioAFrente = transform.TransformPoint(0.5f, 0.0f, 0.0f);
            RaycastHit2D surfaceHit = Physics2D.Raycast(raioAFrente, Vector2.down, 4f, chao);
            Debug.DrawRay(raioAFrente, dir: transform.TransformDirection(Vector2.down) * 1.75f, color: Color.green);
            if (surfaceHit.collider == null)
            {
                MudaPatrulha();
            }
            if (herovsInimigo < persegue)
            {
                if (herovsInimigo <= disAtaque)
                {
                    Atacar();
                }
                else
                    Persegue();
            }
            else
                Patrulhar();
        }
        
    }

    private void Patrulhar()
    {
        // Verifica se há obstáculos no caminho
        RaycastHit2D obstaculo = Physics2D.Raycast(transform.position, direcao, disPatrulha - (disPatrulha - 1), walLayer);
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
            Move();
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha a linha de patrulha
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pontoInicial, pontoFinal);
    }
    public void Move()
    {
        //MOVIMENTO POR TRANSLATE
        /* // Se não houver obstáculo, move o objeto na direção atual
         AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Run");
         print("Anda");
         transform.Translate(direcao * velocidade * Time.deltaTime, Space.World);
         // Vira o inimigo para a direção do movimento
         transform.localScale = new Vector2(Mathf.Sign(direcao.x), 1f);*/


        //MOVIMENTO POR RIGIDBODY
        // Se não houver obstáculo, move o objeto na direção atual
/*        AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Run");
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
                    pontoInicial.x = transform.position.x;
                    pontoFinal.x = transform.position.x;
                    pontoFinal.x = (transform.position.x - 1f);
                    pontoInicial.x = (pontoInicial.x - disPatrulha);
                    //print("Direita");
                }
                else
                {
                    pontoInicial.x = transform.position.x;
                    pontoFinal.x = transform.position.x;
                    pontoInicial.x = (transform.position.x + 1f);
                    pontoFinal.x = (pontoFinal.x + disPatrulha);

                    //print("Esquerda");
                }
                indoParaDireita = !indoParaDireita;
                fechadura = true;
        }
    }
    public void TakeDemage(int damage)
    {
        //animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            //animator.SetBool("Die", true);
            isDead = true;
            //rb2d.velocity = direcao * 0;
            //currentHealth = 0;
            this.enabled = false;
            Destroy(this.gameObject, tempoDeMorte);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Inimigo>().enabled = false;
        }
    }
<<<<<<< HEAD:Projeto educa/Assets/Scripts/Inimigos/InimigoScriptReserva.cs
    public void Atacar()
=======
    void ChangeAnimState(int newState)
>>>>>>> 314472e8e0aa23f17ece3165ab80b356645b4262:Projeto educa/Assets/Scripts/Inimigo.cs
    {
        if (posHero.x > posInimigo.x)
        {
            indoParaDireita = true;
            transform.localScale = new Vector2(Mathf.Sign(direcao.x), 1f);
            if ((posHero.x > posInimigo.x && Vector2.Dot(transform.right, direcao) > 0))
            {
                AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
                rd.velocity = direcao * 0;
            }
        }
        else
        {
            indoParaDireita = false;
            transform.localScale = new Vector2(Mathf.Sign(direcao.x), 1f);
            if ((posHero.x < posInimigo.x && Vector2.Dot(transform.right, direcao) < 0))
            {
                AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
                rd.velocity = direcao * 0;
            }
        }
    }
}*/