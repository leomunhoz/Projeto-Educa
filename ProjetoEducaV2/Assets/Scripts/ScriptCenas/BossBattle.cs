using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public GameObject bossPrefab;
    public Vector2 spawnPointHeroi;
    public Vector2 posHero;
    public GameObject heroiPrefab;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        bossPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/bossPrefab");
        GameObject boss = Instantiate(bossPrefab, new Vector2(11.23f, 3f), Quaternion.identity);
        Criaturas.BossGoblin bossCriatura = new Criaturas.BossGoblin();
        BossComportamento bossScript = boss.GetComponent<BossComportamento>();
        bossScript.Parametros(bossCriatura.Nome, bossCriatura.Dano, bossCriatura.Defesa, bossCriatura.DisPersegue, bossCriatura.DisAtaque, bossCriatura.DisPatrulha, bossCriatura.Velocidade, bossCriatura.vidaTotal, bossCriatura.Coin);


        /*Arrumar*/
        heroiPrefab = Resources.Load<GameObject>("Prefab/PrefabHeroi/HeroiPrefab");
        spawnPointHeroi = GameObject.Find("SpawnPointHeroi").transform.position;
        GameObject heroi = Instantiate(heroiPrefab, spawnPointHeroi, Quaternion.identity);
        Criaturas.Heroi heroiCriatura = new Criaturas.Heroi();
        PlayerOne heroiScript = heroi.GetComponent<PlayerOne>();
        heroiScript.Parametros(heroiCriatura.contMortos, heroiCriatura.Dano, heroiCriatura.vida, heroiCriatura.Defesa);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        posHero = new Vector2(player.transform.position.x, player.transform.position.y);
    }
}
