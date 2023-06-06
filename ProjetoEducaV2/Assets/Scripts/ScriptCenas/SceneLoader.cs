using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public int cena;

    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameManager.Instance.PlayMusic("Menu");
        }
    }

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
