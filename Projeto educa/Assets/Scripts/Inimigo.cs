using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inimigo : MonoBehaviour
{
    public float velocidade = 2f;
    public float distanciaPatrulha = 5f;
    public LayerMask walLayer;
    public LayerMask chao;

    private Vector2 raioAFrente;
    public Vector2 pontoInicial;
    public Vector2 pontoFinal;
    private bool indoParaDireita = true;

    public int maxHealth = 100;
    public int currentHealth;
    public float tempoDeMorte = 20f;
    bool isDead = false;

    public Animator animator;
    public GameObject player;
    public Vector2 posHero;
    public Vector2 posInimigo;
    public Vector2 direcao;
    public float herovsInimigo;
    public float persegue;
    public float ataque;
    public float tempoDeEspera = 2f;
    public bool fechadura=false;
    //
    //public GameObject slime;
    //public Vector2 slimePos;
    //public float slimeDis;
    //
    private void Start()
    {
        pontoInicial = transform.position;
        pontoFinal = pontoInicial + Vector2.right * distanciaPatrulha;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        persegue = 5f;
        walLayer =  LayerMask.GetMask("Wall");
        chao = LayerMask.GetMask("chao");
        ataque = 3f;
        currentHealth = maxHealth;
        //slime = GameObject.Find("Slime All Animations_0");
    }

    private void Update()
    {
        direcao = indoParaDireita ? Vector2.right : Vector2.left;
        posHero = new Vector2(player.transform.position.x, player.transform.position.y);
        //slimePos = new Vector2(slime.transform.position.x, slime.transform.position.y);
        posInimigo = new Vector2(transform.position.x,transform.position.y);
        herovsInimigo = Vector2.Distance(posHero, posInimigo);
        //slimeDis = Vector2.Distance(slimePos, posInimigo);
       // print("Entre Hero e Inimigo"+herovsInimigo);
        //print("Entre Slime e Inimigo" + slimeDis);
        raioAFrente = transform.TransformPoint(0.5f, 0.0f, 0.0f);
        RaycastHit2D surfaceHit = Physics2D.Raycast(raioAFrente, Vector2.down, 1.5f, chao);
        Debug.DrawRay(raioAFrente, dir: transform.TransformDirection(Vector2.down) * 1.5f, color: Color.green);
        if (surfaceHit.collider == null)
        {
            MudaPatrulha();
        }


        if (herovsInimigo < persegue)
        {
            Persegue();
        }
        else
            Patrulhar();
    }

    private void Patrulhar()
    {
        // Verifica se há obstáculos no caminho
        RaycastHit2D obstaculo = Physics2D.Raycast(transform.position, direcao, distanciaPatrulha-4f, walLayer);
        Debug.DrawRay(posInimigo, dir: direcao * 2.0f, color: Color.red);
        {
            if (obstaculo.collider != null)
            {
                // Se houver obstáculo, muda a direção
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
        if (herovsInimigo<= ataque)
        {
            animator.SetTrigger("Idle");
            print("Boing");
        }
        else
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
        
        // Se não houver obstáculo, move o objeto na direção atual
        animator.SetTrigger("Run");
        transform.Translate(direcao * velocidade * Time.deltaTime, Space.World);
        // Vira o inimigo para a direção do movimento
        transform.localScale = new Vector2(Mathf.Sign(direcao.x), 1f);
    }
    public void MudaPatrulha()
    {
        if (!fechadura)//Em contado com Patrulha()
            {
                print("Volta");
                if (indoParaDireita)
                {
                    pontoInicial.x = transform.position.x;
                    pontoFinal.x = transform.position.x;
                    pontoFinal.x = (transform.position.x - 1f);
                    pontoInicial.x = (pontoInicial.x - distanciaPatrulha);
                    //indoParaDireita = !indoParaDireita;
                    print("Direita");
                }
                else
                {
                    pontoInicial.x = transform.position.x;
                    pontoFinal.x = transform.position.x;
                    pontoInicial.x = (transform.position.x + 1f);
                    pontoFinal.x = (pontoFinal.x + distanciaPatrulha);

                    print("Esquerda");
                }
                indoParaDireita = !indoParaDireita;
                fechadura = true;
        }
    }
    public void TakeDemage(int damage)
    {
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            animator.SetBool("Die", true);
            isDead = true;
            //rb2d.velocity = direcao * 0;
            //currentHealth = 0;
            this.enabled = false;
            Destroy(this.gameObject, tempoDeMorte);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Inimigo>().enabled = false;
        }
    }
}