using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : IPresistentSingleton<GameManager>
{

    public GameObject[] inimigos;//=new GameObject[57];
    public Vector2[] spawns; //= new Vector2[57];
    //public GameObject[] spawnPointInimigo= new GameObject[53];
    public bool temMorto=true;
    //public GameObject bossPrefab;
    //public Vector2 spawnPointHeroi;
    public Vector2 posHero;
    //public GameObject heroiPrefab;
    public GameObject player;
    bool cenaCarregada = false;
    //Mapa1 mapa1;
    private void Start()
    {
       
    }
    public void Update()
    {
        if (SceneManager.GetSceneByName("Gameplay").isLoaded && cenaCarregada==false)
        {
            /*mapa1 = FindObjectOfType<Mapa1>();
            spawnPointHeroi = GameObject.Find("SpawnPointHeroi").transform.position;
            //GameObject heroi = Instantiate(heroiPrefab, spawnPointHeroi, Quaternion.identity);
            GameObject heroi = Instantiate(heroiPrefab, new Vector2(56.01263f, 28.315f), Quaternion.identity);
            Criaturas.Heroi heroiCriatura = new Criaturas.Heroi();
            PlayerOne heroiScript = heroi.GetComponent<PlayerOne>();
            heroiScript.Parametros(heroiCriatura.contMortos, heroiCriatura.Dano, heroiCriatura.vida, heroiCriatura.Defesa);*/
            player = GameObject.FindGameObjectWithTag("Player");
            cenaCarregada = true;
        }
        if (cenaCarregada)
            posHero = new Vector2(player.transform.position.x, player.transform.position.y);
    }
}
