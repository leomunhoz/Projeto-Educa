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

}
