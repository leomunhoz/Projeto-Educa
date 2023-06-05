using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : IPresistentSingleton<GameManager>
{
    [HideInInspector]
    public GameObject[] inimigos;//=new GameObject[57];
    [HideInInspector]
    public Vector2[] spawns; //= new Vector2[57];
    public bool temMorto=true;

    ////LoadLevelTransition/////
    public float transitionTime = 1f;
    public int index;
    public Animator animator;
    public Canvas canvas;


    public void LoadNextLevel() 
    {
       StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
       
    }
    public void LoadIndexLvl(int index) 
    {
        
        StartCoroutine(LoadLevel(index));
    }
    IEnumerator LoadLevel(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
        



    }
}
