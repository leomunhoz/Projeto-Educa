using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public int cena;

    public void Start()
    {
        GameManager.Instance.PlayMusic("Menu");
# if UNITY_EDITOR
        Debug.Log("MenuIniciado");
#endif
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
