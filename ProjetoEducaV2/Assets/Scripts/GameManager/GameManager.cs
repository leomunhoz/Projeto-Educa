using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : IPresistentSingleton<GameManager>
{
    [HideInInspector]
    public GameObject[] inimigos;//=new GameObject[57];
    [HideInInspector]
    public Vector2[] spawns; //= new Vector2[57];
    public bool temMorto=true;

    [Header("LoadScene")]
    public float transitionTime = 1f;
    public int index;
    public Animator animator;
    public Canvas canvas;
    [Header("Audio")]
    public Sound[] musicSounds, sfxSounds;
    public AudioClip[] footStep;
    public AudioSource musicSource, sfxSource;

    public void LoadNextLevel() 
    {
       StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
       
    }
    public void LoadIndexLvl(int index) 
    {
        
        StartCoroutine(LoadLevel(index));
    }
    public void Exit() 
    {
        Application.Quit();
    }
    IEnumerator LoadLevel(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
        



    }
    //////////////////////////////////////
    //////////////////////////////////////
    public void PlayMusic(string name) 
    {
        Sound s = Array.Find(musicSounds, x => x.nome == name);
        if (s == null)
        {
            Debug.Log("Musica não encontrada");
            
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name) 
    {
        Sound s = Array.Find(sfxSounds, x => x.nome == name);
        if (s == null)
        {
            Debug.Log("Musica não encontrada");
            
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    public void PlayFootStep() 
    {
        sfxSource.PlayOneShot(footStep[UnityEngine.Random.Range(0, footStep.Length)]);
    }
}
