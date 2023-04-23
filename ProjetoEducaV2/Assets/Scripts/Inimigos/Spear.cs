using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public  Rigidbody2D rbd2;
    public  Collider2D[] playerHits;
    public Collider2D[] chaoHits;
    public  LayerMask playerLayer;
    public LayerMask Wall;
    public  Transform spearAttack;
    public  float attackRange;
    public int damage = 10;
    public bool right = false;
    public bool fecha = false;
    public float inicial;
    public static bool lado;
    public float disMax=10f;
   
    public static void Direcao(bool direita)
    {
        lado = direita;
    }

    private void Awake()
    {
        rbd2 = GetComponent<Rigidbody2D>();
        if (lado)
            right = true;
        else
            right = false;
        inicial = spearAttack.position.x;
    }
    private void Update()
    {
        playerHits = Physics2D.OverlapCircleAll(spearAttack.position, attackRange, playerLayer);
        foreach (var Player in playerHits) 
        {
            Player.GetComponent<PlayerOne>().TakeDamage(damage);
            Destroy(this.gameObject);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Spear>().enabled = false;
        }
        chaoHits = Physics2D.OverlapCircleAll(spearAttack.position, attackRange, Wall);
        foreach (var Chao in chaoHits)
        {
            Destroy(this.gameObject);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Spear>().enabled = false;
        }
        if (!right)
        {
            rbd2.velocity = new Vector2(-5, 0.0f);
            transform.localScale = new Vector2(Mathf.Sign(-1), 1f);
           
        }
        else
        {
            rbd2.velocity = new Vector2(5, 0.0f);
            transform.localScale = new Vector2(Mathf.Sign(1), 1f);
            
        }
        if (spearAttack.position.x>(inicial+disMax) || spearAttack.position.x < (inicial - disMax)||right!=lado)
        {
            Destroy(this.gameObject);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Spear>().enabled = false;

        }
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spearAttack.position, attackRange);
    }
}

