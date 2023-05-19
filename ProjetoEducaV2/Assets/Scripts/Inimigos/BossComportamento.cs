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
public class BossComportamento : MonoBehaviour
{
    //public GameObject player;
    public PlayerOne play;
    public GameObject Projetil;
    public LayerMask walLayer;
    public LayerMask chao;
    public LayerMask PlayerLayer;
    public RaycastHit2D playerHitFrente;
    public RaycastHit2D playerOUParece;
    public RaycastHit2D obstaculo;
    public Collider2D[] playerHits;

    //public Vector2 posHero;//Sugeito a GameManager
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
    public bool fechaPulo=false;


    public float posY;
    public float disPlayerRay;
    public float disParede;
    public float disChao;
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
    public int grana;
    public string nome;
    public float duracaoAnimacao;

    public Rigidbody2D rd;
    public float tempoDecorrido = 0f;
    public float puloCD = 5f;
    public float gritoCD = 5f;
    public bool podeGritar = true;
    public bool podePular = true;
    public float tempoPulo = 0f;
    public float tempoGrito = 0f;

    float tempoAtaque = 0f;
    float ataqueCD = 0.1f;
    bool podeAtacar = true;

    BossBattle bossBattle;


    /* public float radius;
     public GameObject animacaoDanoPrefab;
     public LayerMask playerLayer;*/
    public void Parametros(string nomeC, int danoC, int def, float Persegue, float Ataque, float Patrulha, float velo, float vidaToda, int moeda)
    {
        disPersegue = Persegue;
        disAtaque = Ataque;
        disPatrulha = Patrulha;
        velocidade = velo;
        vidaTotal = vidaToda;
        defesa = def;
        dano = danoC;
        nome = nomeC;
        grana = moeda;
    }

    private void Start()
    {

        bossBattle = FindObjectOfType<BossBattle>();
        //print("Nome=" + nome);
        pontoInicial = transform.position;
        pontoFinal = pontoInicial + Vector2.right * disPatrulha;
        //player = GameObject.FindGameObjectWithTag("Player");
        walLayer = LayerMask.GetMask("Chao");
        chao = LayerMask.GetMask("Chao", "Platform");
        PlayerLayer = LayerMask.GetMask("Player");
        currentHealth = vidaTotal;
        rd = GetComponent<Rigidbody2D>();
        //mapa1 = mapa1.GetComponent<Mapa1>();

    }

    private void Update()
    {
        if (!isDead)
        {
            direcao = indoParaDireita ? Vector2.right : Vector2.left;
            //posHero = new Vector2(player.transform.position.x, player.transform.position.y);
            posInimigo = new Vector2(transform.position.x, transform.position.y);
            herovsInimigo = Vector2.Distance(bossBattle.posHero, posInimigo);
           /* 
            rd.velocity = direcao * velocidade;
            // Vira o inimigo para a direção do movimento
            transform.localScale = new Vector2(indoParaDireita ? 1f : -1f, 1f);*/
            //print(posY);

            raioAFrente = transform.TransformPoint(-0.5f, 0.0f, 0.0f);
            RaycastHit2D surfaceHit = Physics2D.Raycast(raioAFrente, Vector2.down, 4f, chao);
            Debug.DrawRay(raioAFrente, dir: transform.TransformDirection(Vector2.down) * 1.75f, color: Color.green);
            disChao = surfaceHit.distance;
            if (disChao < 1f)
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.0001f);
            playerHitFrente = Physics2D.Raycast(transform.position, direcao, disPersegue, PlayerLayer);
            Debug.DrawRay(posInimigo, dir: direcao * disPersegue, color: Color.yellow);
            disPlayerRay = playerHitFrente.distance;

            playerOUParece = Physics2D.Raycast(transform.position, direcao, 1000, walLayer);
            disParede = playerOUParece.distance;

            posY = Mathf.Abs(posInimigo.y) - Mathf.Abs(bossBattle.posHero.y);

            CalculaCds();

            if (disPlayerRay == 0)
                disPlayerRay = disParede + 1f;

            if (surfaceHit.collider == null)
            {
               // MudaPatrulha();

            }
            /*if ((disParede < disPlayerRay))
                parede = true;
            else
                parede = false;*/
            if (herovsInimigo < disPersegue)
            {
                //if (Mathf.Abs(Mathf.Abs(posInimigo.y) - Mathf.Abs(posHero.y)) < 2)
                if (herovsInimigo <= disAtaque && Mathf.Abs(posY) < 3)
                {
                    if (tempoAtaque == 0f)
                        Atacar();
                }
                else
                {
                    if (tempoPulo == 0f)
                    {
                        StartCoroutine(Pular());//PuloMortal();
                        
                    }
                    else
                        AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Screen");
                    emAtaque = false;
                }
            }
            else
            {
                if (tempoGrito==0f)
                    Grito();
                else
                    AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Idle");
                emAtaque = false;
            }

        }
    }
    private void FixedUpdate()
    {

        // playerHitCosta = Physics2D.Raycast(transform.position, Vector2.left,disPersegue, PlayerLayer);
    }

  /*  private void Patrulhar()
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
    }*/
   /* public void Persegue()

    {

        if (herovsInimigo >= disAtaque && Mathf.Abs(posY) < 2)
        {
            ViraParaPlayer();
            Move();
        }
        else
        {
            AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Idle");
            rd.velocity = direcao * 0;
        }


    }*/

    private void OnDrawGizmosSelected()
    {
        // Desenha a linha de patrulha
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pontoInicial, pontoFinal);
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
            play = bossBattle.player.GetComponent<PlayerOne>();
            play.mortos++;
            play.coin = play.coin + grana;
            print(nome + " Mortos " + play.mortos);
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
                    if ((bossBattle.posHero.x > posInimigo.x && Vector2.Dot(transform.right, direcao) > 0))//Está invertido
                    {
                        {
                            emAtaque = true;
                            AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
                            rd.velocity = direcao * 0;
                            MelleDano(disAtaque);
                        }
                    }
                }
            }
            else//Esquerda
            {

                if (!emAtaque)
                {
                    //AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Idle");
                    if ((bossBattle.posHero.x < posInimigo.x && Vector2.Dot(transform.right, direcao) < 0))//Está invertido
                    {
                        {
                            emAtaque = true;
                            AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
                            rd.velocity = direcao * 0;
                            MelleDano(disAtaque);
                        }
                    }
                }
            }
        }
    }
    public Vector2 ViraParaPlayer()
    {
        if (bossBattle.posHero.x > posInimigo.x)
        {
            indoParaDireita = true;
            transform.localScale = new Vector2(-1f, 1f);
        }
        else//Esquerda
        {

            indoParaDireita = false;
            transform.localScale = new Vector2(1f, 1f);

        }
        direcao = indoParaDireita ? Vector2.right : Vector2.left;

        //transform.localScale = new Vector2(indoParaDireita ? 1f : -1f, 1f);
        playerHitFrente = Physics2D.Raycast(transform.position, direcao, disPersegue, PlayerLayer);
        disPlayerRay = playerHitFrente.distance;
        playerOUParece = Physics2D.Raycast(transform.position, direcao, 1000, walLayer);
        disParede = playerOUParece.distance;

        emAtaque = false;
        return direcao;
    }
    public bool AtualizaInimigo(bool atualizado)
    {
        return true;
    }

    public void Grito()
    {
        AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Screen");
    }
    public void PuloMortal()
    {
        AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Jump");
        float duracaoAnimacao = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        print("duracaoAnimacao" + duracaoAnimacao);
    }
    IEnumerator Pular()
    {
        Animator animator = GetComponent<Animator>();
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Calcule a posição final do salto
        animator.Play("Jump");
        if (fechaPulo == false)
        {
            pontoInicial = rd.position;
            pontoFinal = new Vector2(bossBattle.posHero.x, bossBattle.posHero.y+8);
            fechaPulo = true;
            tempoDecorrido = 0f;
        }
        duracaoAnimacao = 2f;// GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        // Inicie o salto
        
        
        while (tempoDecorrido < duracaoAnimacao)
        {
            float t = tempoDecorrido / duracaoAnimacao;
            rd.position = Vector2.Lerp(pontoInicial, pontoFinal, t);
            tempoDecorrido += Time.deltaTime;
            yield return null;
            
        }
        if (tempoDecorrido>= duracaoAnimacao)
        {
            fechaPulo = false;//controle local
            podePular = false;//Começa o CD para pular.
        }
            
        // Finalize o salto
        rd.AddForce(new Vector2(0f, 1), ForceMode2D.Impulse);
        if (tempoDecorrido < duracaoAnimacao)
        {
            StartCoroutine(Pular());
        }
        // Implemente a lógica de ataque ao jogador após o salto
        MelleDano(1);
    }
    public void CalculaCds()
    {
        if (podePular == false)
        {
            tempoPulo += Time.deltaTime;
        }
        if (tempoPulo >= puloCD)
        {
            podePular = true;
            tempoPulo = 0f;
        }
        if (podeGritar == false)
        {
            tempoGrito += Time.deltaTime;
        }
        if (tempoGrito >= gritoCD)
        {
            podeGritar = true;
            tempoGrito = 0f;
        }
        if (podeAtacar == false)
        {
            tempoAtaque += Time.deltaTime;
        }
        if (tempoAtaque >= ataqueCD)
        {
            podeAtacar = true;
            tempoAtaque = 0f;
        }
    }
    public void MelleDano(float distancia)
    {
        playerHits = Physics2D.OverlapCircleAll(transform.position, distancia, LayerMask.GetMask("Player"));
        foreach (var Player in playerHits)
        {
            Player.GetComponent<PlayerOne>().TakeDamage(dano);
        }
    }
}