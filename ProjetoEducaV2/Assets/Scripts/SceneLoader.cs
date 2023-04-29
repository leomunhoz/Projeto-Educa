using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private void Awake()
    {
        GameManager tem = GameManager.Instance;
    }



    public void loadscene(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    public void RestartScene(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
