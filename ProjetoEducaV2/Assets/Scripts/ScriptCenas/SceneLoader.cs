using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader 
{
    public int cena;
    


    public void loadscene(int cena)
    {
        GameManager.Instance.LoadIndexLvl(cena);
    }

    public void RestartScene(int cena)
    {
        GameManager.Instance.LoadIndexLvl(cena);
    }

    public void Exit()
    {
        GameManager.Instance.Exit();
    }
}
