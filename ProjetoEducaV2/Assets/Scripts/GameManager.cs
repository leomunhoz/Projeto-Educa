using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : IPresistentSingleton<GameManager>
{
    
    public GameObject[] inimigos=new GameObject[53];
    //public GameObject[] spawnPointInimigo= new GameObject[53];
    public bool temMorto=true;
    private void Start()
    {
        
    }

}
