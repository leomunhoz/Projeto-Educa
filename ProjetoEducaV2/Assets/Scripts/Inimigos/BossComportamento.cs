using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossComportamento : MonoBehaviour
{
    public Vector2 posHero;
    public Vector2 posInimigo;
    public GameObject player;
    public Animator animator;
    public float herovsInimigo;
    //Quando herovxInimigo < 8.22 é possivel ver toda a cabeça do Boss
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //posHero = new Vector2(player.transform.position.x, player.transform.position.y);
        posInimigo = new Vector2(transform.position.x, transform.position.y);
        herovsInimigo = Vector2.Distance(posHero, posInimigo);
        AnimaInimigo.ChangeAnimState(GetComponent<Animator>(), "Jump");
    }
}
