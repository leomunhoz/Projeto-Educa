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
    private int randomInt;
    private int i = 0;
    GameManager gameManager;
    private float tempoDerespawn = 21f;//não pode ser menor que 20
    private float tempo = 0f;
    public Vector2 posHero;
    private bool temNull=false;
    private Queue<int> posArray = new Queue<int>();
    private Queue<Vector2> local = new Queue<Vector2>();
    void Awake()
    {
        heroiPrefab = Resources.Load<GameObject>("Prefab/PrefabHeroi/HeroiPrefab");
        spawnPointHeroi = GameObject.Find("SpawnPointHeroi").transform.position;
        GameObject heroi = Instantiate(heroiPrefab, spawnPointHeroi, Quaternion.identity);
        //GameObject heroi = Instantiate(heroiPrefab, new Vector2(56.01263f, 28.315f), Quaternion.identity);
        Criaturas.Heroi heroiCriatura = new Criaturas.Heroi();
        PlayerOne heroiScript = heroi.GetComponent<PlayerOne>();
        heroiScript.Parametros(heroiCriatura.contMortos, heroiCriatura.Dano, heroiCriatura.vida, heroiCriatura.Defesa);
        player = GameObject.FindGameObjectWithTag("Player");

        gameManager = GameManager.Instance;
        //Inicio Inimigos
        spawnPointInimigo = GameObject.FindGameObjectsWithTag("Spawn");
        foreach (GameObject spawnPoint in spawnPointInimigo)
         {
            gameManager.spawns[i]= spawnPointInimigo[i].transform.position;
            i++;
         }
        i = 0;
        foreach (Vector2 gasta in gameManager.spawns)
        {
            RandomCreature(gasta,i);
            i++;
        }
            /*for (int i = 0; i <= spawnPointInimigo.Length; i++)
            {
                GameObject spawnPoint = spawnPointInimigo[i];
                Debug.Log("Spawning enemy at spawn point " + i);
                RandomCreature(i);
            }*/

            /*bossPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/bossPrefab");
            GameObject boss = Instantiate(bossPrefab, new Vector2(-96.04f, 2f), Quaternion.identity);
             Criaturas.BossGoblin bossCriatura = new Criaturas.BossGoblin();
             BossComportamento bossScript = boss.GetComponent<BossComportamento>();
             bossScript.Parametros(bossCriatura.Nome, bossCriatura.Dano, bossCriatura.Defesa, bossCriatura.DisPersegue, bossCriatura.DisAtaque, bossCriatura.DisPatrulha, bossCriatura.Velocidade, bossCriatura.vidaTotal, bossCriatura.Coin);*/
            gameManager.temMorto = false;
        //print("Quantidade de inimigos: " + i);
        i = 0;
        //spawnPointInimigo = null;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameManager.temMorto == true || temNull==true)
        {
            tempo += Time.deltaTime;
            if (tempo >= tempoDerespawn)
            {
                tempo = 0f;
                gameManager.temMorto = false;
                //print("Posição: "+posArray.Peek());
                //print(gameManager.spawns[posArray.Peek()]);
                //print("Voltei para Matar em: "+ local.Peek());
                RandomCreature(local.Peek(), posArray.Peek());//Colocar a posição 0 aqui
                posArray.Dequeue();
                local.Dequeue();
                gameManager.temMorto = false;
                ReporMorte(-1,Vector2.zero);
            }
        }
        /*for (int i = 0; i <= gameManager.spawns.Length-1;i++)
         {
             print("aqui" + gameManager.spawns[i]);
         }*/
        posHero = new Vector2(player.transform.position.x, player.transform.position.y);
    }
    public void ReporMorte(int posicao, Vector2 ondeNasce)
    {
        if (posicao>-1)
        {
            posArray.Enqueue(posicao);
            local.Enqueue(ondeNasce);
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
    public void RandomCreature(Vector2 spawnPoint, int posicao)
    {
        /*if (spawnPointInimigo[posicao] == null)
        {
            Debug.Log("O objeto foi destruído.");
            return;
        }*/
        randomInt = Random.Range(0, 3);
        if (randomInt == 0)
        {
            goblinPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/goblinPrefab");
            GameObject goblin = Instantiate(goblinPrefab, spawnPoint, Quaternion.identity);
            Criaturas.Goblin goblinCriatura = new Criaturas.Goblin();
            Inimigo goblinScript = goblin.GetComponent<Inimigo>();
            goblinScript.Parametros(goblinCriatura.Nome, goblinCriatura.Dano, goblinCriatura.Defesa, goblinCriatura.DisPersegue, goblinCriatura.DisAtaque, goblinCriatura.DisPatrulha, goblinCriatura.Velocidade, goblinCriatura.vidaTotal, goblinCriatura.Coin, i);
            gameManager.inimigos[posicao] = goblin;
        }
        else if (randomInt == 1)
        {
            slimePrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/slimePrefab");
            GameObject slime = Instantiate(slimePrefab, spawnPoint, Quaternion.identity);
            Criaturas.Slime slimeCriatura = new Criaturas.Slime();
            Inimigo slimeScript = slime.GetComponent<Inimigo>();
            slimeScript.Parametros(slimeCriatura.Nome, slimeCriatura.Dano, slimeCriatura.Defesa, slimeCriatura.DisPersegue, slimeCriatura.DisAtaque, slimeCriatura.DisPatrulha, slimeCriatura.Velocidade, slimeCriatura.vidaTotal, slimeCriatura.Coin, i);
            gameManager.inimigos[posicao] = slime;
        }
        else
        {
            batPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/batPrefab");
            GameObject bat = Instantiate(batPrefab, spawnPoint, Quaternion.identity);
            Criaturas.Bat batCriatura = new Criaturas.Bat();
            Inimigo batScript = bat.GetComponent<Inimigo>();
            batScript.Parametros(batCriatura.Nome, batCriatura.Dano, batCriatura.Defesa, batCriatura.DisPersegue, batCriatura.DisAtaque, batCriatura.DisPatrulha, batCriatura.Velocidade, batCriatura.vidaTotal, batCriatura.Coin, i);
            gameManager.inimigos[posicao] = bat;
        }
    }
}
