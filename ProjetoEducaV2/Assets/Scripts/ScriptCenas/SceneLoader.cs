using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int cena;
    private void Awake()
    {
        GameManager tem = GameManager.Instance;
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
        Application.Quit();
    }
}
