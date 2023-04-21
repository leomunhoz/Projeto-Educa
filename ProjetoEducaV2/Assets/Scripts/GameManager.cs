using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IPresistentSingleton<GameManager>
{
    // Start is called before the first frame update
  /*  public GameObject slimePrefab;
    public GameObject batPrefab;*/
    public GameObject goblinPrefab;
    public Transform spawnPoint;


    public int numGoblins=5;
    void Start()
    {
        print("Começa");
     /*   slimePrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/slimePrefab");
        batPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/batPrefab");*/
        goblinPrefab = Resources.Load<GameObject>("Prefab/PrefabInimigos/goblinPrefab");
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        

        /*   GameObject slime = Instantiate(slimePrefab, new Vector2(3, 12.85f), Quaternion.identity);
           Criaturas.Slime slimeCriatura = new Criaturas.Slime();
           Inimigo slimeScript = slime.GetComponent<Inimigo>();
           slimeScript.Parametros(slimeCriatura.Nome, slimeCriatura.DisPersegue, slimeCriatura.DisAtaque, slimeCriatura.DisPatrulha, slimeCriatura.Velocidade, slimeCriatura.vidaTotal);

           GameObject bat = Instantiate(batPrefab, new Vector2(3, 12.85f), Quaternion.identity);
           Criaturas.Bat batCriatura = new Criaturas.Bat();
           Inimigo batScript = bat.GetComponent<Inimigo>();
           batScript.Parametros(batCriatura.Nome, batCriatura.DisPersegue, batCriatura.DisAtaque, batCriatura.DisPatrulha, batCriatura.Velocidade, batCriatura.vidaTotal);

           */
        GameObject goblin = Instantiate(goblinPrefab, spawnPoint.transform.position, Quaternion.identity);

 //       GameObject goblin = Instantiate(goblinPrefab, new Vector2(20f, 0f), Quaternion.identity);
        Criaturas.Goblin goblinCriatura = new Criaturas.Goblin();
        Inimigo goblinScript = goblin.GetComponent<Inimigo>();
        goblinScript.Parametros(goblinCriatura.Nome, goblinCriatura.DisPersegue, goblinCriatura.DisAtaque, goblinCriatura.DisPatrulha, goblinCriatura.Velocidade, goblinCriatura.vidaTotal);

        
        //GameObject goblin1 = Instantiate(goblinPrefab, new Vector2(5, 12.85f), Quaternion.identity);
        //Criaturas.Goblin goblinCriatura1 = new Criaturas.Goblin();
        //Inimigo goblinScript1 = new Inimigo();
        //print("goblinCriatura1.Nome=" + goblinCriatura1.Nome);
        //print("goblinCriatura1.DisPersegue=" + goblinCriatura1.DisPersegue);
        //print("goblinCriatura1.DisAtaque=" + goblinCriatura1.DisAtaque);
        //print("goblinCriatura1.DisPatrulha=" + goblinCriatura1.DisPatrulha);
        //print("goblinCriatura1.Velocidade=" + goblinCriatura1.Velocidade);
        //print("goblinCriatura1.vidaTotal=" + goblinCriatura1.vidaTotal);

        //goblinScript1.Parametros(goblinCriatura1.Nome, goblinCriatura1.DisPersegue, goblinCriatura1.DisAtaque, goblinCriatura1.DisPatrulha, goblinCriatura1.Velocidade, goblinCriatura1.vidaTotal);

        for (int i = 0; i < numGoblins; i++)
        {
        
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
