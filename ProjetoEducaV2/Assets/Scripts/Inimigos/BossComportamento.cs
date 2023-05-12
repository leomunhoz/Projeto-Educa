using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class BossComportamento : MonoBehaviour
{
    private Vector2 raioAFrente;
    public Vector2 posInimigo;
    public LayerMask chao;
    public GameObject player;
    public Animator animator;
    public float herovsInimigo;
    public float posY;
    public float vidaTotal;//virá do construct
    public float disPersegue;//virá do construct
    private float disAtaque;//virá do construct
    public float velocidade;//virá do construct
    public float disPatrulha;//virá do construct
    public int dano;//virá do construct
    public int defesa;//virá do construct
    public int grana;//virá do construct
    public string nome;//virá do construct

    public void Parametros(string nomeC, int danoC, int def, float Persegue, float Ataque, float Patrulha, float velo, float vidaToda, int moeda)
    {
        disPersegue = 8.22f;//Persegue;
        disAtaque = Ataque;
        disPatrulha = Patrulha;
        velocidade = velo;
        vidaTotal = vidaToda;
        defesa = def;
        dano = danoC;
        nome = nomeC;
        grana = moeda;
    }
    //Quando herovxInimigo < 8.22 é possivel ver toda a cabeça do Boss
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //posHero = new Vector2(player.transform.position.x, player.transform.position.y);
        posInimigo = new Vector2(transform.position.x, transform.position.y);
        herovsInimigo = Vector2.Distance(Mapa1.posHero, posInimigo);
        //AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Jump");
        posY = Mathf.Abs(posInimigo.y) - Mathf.Abs(Mapa1.posHero.y);

        raioAFrente = transform.TransformPoint(0.5f, 0.0f, 0.0f);
        RaycastHit2D surfaceHit = Physics2D.Raycast(raioAFrente, Vector2.down, 4f, chao);

        //Inicia Ataques
        if (herovsInimigo < disPersegue)
        {
            if (herovsInimigo <= disAtaque && Mathf.Abs(posY) < 2)
            {
                Clava();
            }
            else
            {
                PuloMortal();
            }
        }
        else
        {
            Berro();

        }
    
    }
    public void PuloMortal()
    {
        AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Jump");
    }
    public void Berro()
    {
        AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Screen");
    }
    public void Clava()
    {
        AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Attack");
    }
}
