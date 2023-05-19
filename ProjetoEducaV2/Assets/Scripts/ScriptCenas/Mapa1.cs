using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mapa1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject slimePrefab;
    public GameObject batPrefab;
    public GameObject goblinPrefab;
    public GameObject heroiPrefab;
    public GameObject bossPrefab;
    public GameObject[] spawnPointInimigo;
    public Vector2 spawnPointHeroi;
    public Vector2 posHero;
    private int randomInt;
    private int i = 0;
    GameManager gameManager;
    private float tempoDerespawn = 60f;
    private float tempo = 0f;
    private bool temNull=false;
    private Queue<int> numeros = new Queue<int>();
    void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager != null)
        {
            
        }


        heroiPrefab = Resources.Load<GameObject>("Prefab/PrefabHeroi/HeroiPrefab");
        spawnPointHeroi = GameObject.Find("SpawnPointHeroi").transform.position;
        GameObject heroi = Instantiate(heroiPrefab, spawnPointHeroi, Quaternion.identity);
        Criaturas.Heroi heroiCriatura = new Criaturas.Heroi();
        PlayerOne heroiScript = heroi.GetComponent<PlayerOne>();
        heroiScript.Parametros(heroiCriatura.contMortos, heroiCriatura.Dano, heroiCriatura.vida, heroiCriatura.Defesa);
        player = GameObject.FindGameObjectWithTag("Player");



        //Inicio Inimigos
         batPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/batPrefab");
         slimePrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/slimePrefab");
         goblinPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/goblinPrefab");
         spawnPointInimigo = GameObject.FindGameObjectsWithTag("Spawn");
         foreach (GameObject spawnPoint in spawnPointInimigo)
         {
            RandomCreature(i);
            i++;
         }
        
        /*for (int i = 0; i <= spawnPointInimigo.Length; i++)
        {
            GameObject spawnPoint = spawnPointInimigo[i];
            Debug.Log("Spawning enemy at spawn point " + i);
            RandomCreature(i);
        }*/

        bossPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/bossPrefab");
        GameObject boss = Instantiate(bossPrefab, new Vector2(-96.04f, 2f), Quaternion.identity);
         Criaturas.BossGoblin bossCriatura = new Criaturas.BossGoblin();
         BossComportamento bossScript = boss.GetComponent<BossComportamento>();
         bossScript.Parametros(bossCriatura.Nome, bossCriatura.Dano, bossCriatura.Defesa, bossCriatura.DisPersegue, bossCriatura.DisAtaque, bossCriatura.DisPatrulha, bossCriatura.Velocidade, bossCriatura.vidaTotal, bossCriatura.Coin);
        gameManager.temMorto = false;
        print("Quantidade de inimigos: " + i);
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        posHero = new Vector2(player.transform.position.x, player.transform.position.y);
        if (gameManager.temMorto == true || temNull==true)
        {
            tempo += Time.deltaTime;
            if (tempo >= tempoDerespawn)
            {
                tempo = 0f;
                gameManager.temMorto = false;
                RandomCreature(numeros.Peek());//Colocar a posição 0 aqui
                numeros.Dequeue();
                gameManager.temMorto = false;
                ReporMorte(-1);
            }
        }
       /* for (int i = 0; i <= spawnPointInimigo.Length-1;i++)
        {
            Debug.Log(spawnPointInimigo[i]);
            Debug.Log(i);
        }*/
    }
    public void ReporMorte(int posicao)
    {
        if (posicao>-1)
        {
            numeros.Enqueue(posicao);
            gameManager.temMorto = true;
            gameManager.inimigos[posicao] = null;
        }
        
        for(int i = 0; i< gameManager.inimigos.Length; i++)
        {
            if (gameManager.inimigos[i] == null)
            {
                temNull = true;
                return;
            }
            else
                temNull = false;
                
        }
    }
    public void RandomCreature(int posicao)
    {
        randomInt = Random.Range(0, 3);
        if (randomInt == 0)
        {
            GameObject goblin = Instantiate(goblinPrefab, spawnPointInimigo[i].transform.position, Quaternion.identity);
            Criaturas.Goblin goblinCriatura = new Criaturas.Goblin();
            Inimigo goblinScript = goblin.GetComponent<Inimigo>();
            goblinScript.Parametros(goblinCriatura.Nome, goblinCriatura.Dano, goblinCriatura.Defesa, goblinCriatura.DisPersegue, goblinCriatura.DisAtaque, goblinCriatura.DisPatrulha, goblinCriatura.Velocidade, goblinCriatura.vidaTotal, goblinCriatura.Coin, i);
            gameManager.inimigos[i] = goblin;
        }
        else if (randomInt == 1)
        {
            GameObject slime = Instantiate(slimePrefab, spawnPointInimigo[i].transform.position, Quaternion.identity);
            Criaturas.Slime slimeCriatura = new Criaturas.Slime();
            Inimigo slimeScript = slime.GetComponent<Inimigo>();
            slimeScript.Parametros(slimeCriatura.Nome, slimeCriatura.Dano, slimeCriatura.Defesa, slimeCriatura.DisPersegue, slimeCriatura.DisAtaque, slimeCriatura.DisPatrulha, slimeCriatura.Velocidade, slimeCriatura.vidaTotal, slimeCriatura.Coin, i);
            gameManager.inimigos[i] = slime;
        }
        else
        {
            GameObject bat = Instantiate(batPrefab, spawnPointInimigo[i].transform.position, Quaternion.identity);
            Criaturas.Bat batCriatura = new Criaturas.Bat();
            Inimigo batScript = bat.GetComponent<Inimigo>();
            batScript.Parametros(batCriatura.Nome, batCriatura.Dano, batCriatura.Defesa, batCriatura.DisPersegue, batCriatura.DisAtaque, batCriatura.DisPatrulha, batCriatura.Velocidade, batCriatura.vidaTotal, batCriatura.Coin, i);
            gameManager.inimigos[i] = bat;
        }
    }
}
