using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public GameObject bossPrefab;
    // Start is called before the first frame update
    void Start()
    {
        bossPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/bossPrefab");
        GameObject boss = Instantiate(bossPrefab, new Vector2(11.23f, -2.93f), Quaternion.identity);
        Criaturas.BossGoblin bossCriatura = new Criaturas.BossGoblin();
        BossComportamento bossScript = boss.GetComponent<BossComportamento>();
        bossScript.Parametros(bossCriatura.Nome, bossCriatura.Dano, bossCriatura.Defesa, bossCriatura.DisPersegue, bossCriatura.DisAtaque, bossCriatura.DisPatrulha, bossCriatura.Velocidade, bossCriatura.vidaTotal, bossCriatura.Coin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
