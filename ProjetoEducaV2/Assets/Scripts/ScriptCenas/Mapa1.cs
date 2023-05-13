using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa1 : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject player;
    public GameObject slimePrefab;
    public GameObject batPrefab;
    public GameObject goblinPrefab;
    public GameObject heroiPrefab;
    public GameObject bossPrefab;
    public GameObject[] spawnPointInimigo;
    public Vector2 spawnPointHeroi;
    public static Vector2 posHero;
    private int randomInt;
    void Start()
    {
        heroiPrefab = Resources.Load<GameObject>("Prefab/PrefabHeroi/HeroiPrefab");
        spawnPointHeroi = GameObject.Find("SpawnPointHeroi").transform.position;
        GameObject heroi = Instantiate(heroiPrefab, spawnPointHeroi, Quaternion.identity);
        Criaturas.Heroi heroiCriatura = new Criaturas.Heroi();
        PlayerOne heroiScript = heroi.GetComponent<PlayerOne>();
        heroiScript.Parametros(heroiCriatura.contMortos, heroiCriatura.Dano, heroiCriatura.vida, heroiCriatura.Defesa);
        player = GameObject.FindGameObjectWithTag("Player");



        //Inicio Inimigos
        /* batPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/batPrefab");
         slimePrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/slimePrefab");
         goblinPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/goblinPrefab");
         GameObject[] spawnPointInimigo = GameObject.FindGameObjectsWithTag("Spawn");
         foreach (GameObject spawnPoint in spawnPointInimigo)
         {
             randomInt =  Random.Range(0, 3);
             if (randomInt == 0)
             {
                 GameObject goblin = Instantiate(goblinPrefab, spawnPoint.transform.position, Quaternion.identity);
                 Criaturas.Goblin goblinCriatura = new Criaturas.Goblin();
                 Inimigo goblinScript = goblin.GetComponent<Inimigo>();
                 goblinScript.Parametros(goblinCriatura.Nome, goblinCriatura.Dano, goblinCriatura.Defesa, goblinCriatura.DisPersegue, goblinCriatura.DisAtaque, goblinCriatura.DisPatrulha, goblinCriatura.Velocidade, goblinCriatura.vidaTotal, goblinCriatura.Coin);
             }
             else if(randomInt == 1)
             {
                 GameObject slime = Instantiate(slimePrefab, spawnPoint.transform.position, Quaternion.identity);
                 Criaturas.Slime slimeCriatura = new Criaturas.Slime();
                 Inimigo slimeScript = slime.GetComponent<Inimigo>();
                 slimeScript.Parametros(slimeCriatura.Nome, slimeCriatura.Dano, slimeCriatura.Defesa, slimeCriatura.DisPersegue, slimeCriatura.DisAtaque, slimeCriatura.DisPatrulha, slimeCriatura.Velocidade, slimeCriatura.vidaTotal, slimeCriatura.Coin);
             }
             else
             {
                 GameObject bat = Instantiate(batPrefab, spawnPoint.transform.position, Quaternion.identity);
                 Criaturas.Bat batCriatura = new Criaturas.Bat();
                 Inimigo batScript = bat.GetComponent<Inimigo>();
                 batScript.Parametros(batCriatura.Nome, batCriatura.Dano, batCriatura.Defesa, batCriatura.DisPersegue, batCriatura.DisAtaque, batCriatura.DisPatrulha, batCriatura.Velocidade, batCriatura.vidaTotal, batCriatura.Coin);
             }

         }*/
        bossPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/bossPrefab");
        GameObject boss = Instantiate(bossPrefab, new Vector2(-96.04f, 2f), Quaternion.identity);
         Criaturas.BossGoblin bossCriatura = new Criaturas.BossGoblin();
         BossComportamento bossScript = boss.GetComponent<BossComportamento>();
         bossScript.Parametros(bossCriatura.Nome, bossCriatura.Dano, bossCriatura.Defesa, bossCriatura.DisPersegue, bossCriatura.DisAtaque, bossCriatura.DisPatrulha, bossCriatura.Velocidade, bossCriatura.vidaTotal, bossCriatura.Coin);
        
    }
    // Update is called once per frame
    void Update()
    {
        posHero = new Vector2(player.transform.position.x, player.transform.position.y);
    }
}
