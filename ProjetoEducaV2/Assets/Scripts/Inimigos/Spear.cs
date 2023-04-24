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
    public Vector2 direction;
    public  float attackRange;
    public int damage = 10;
    public float inicial;
    public float disMax=10f;
   
    private void Awake()
    {
        rbd2 = GetComponent<Rigidbody2D>();
        inicial = Mathf.Abs(spearAttack.position.x);
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
        if (direction==Vector2.left)
        {
            rbd2.velocity = new Vector2(-5, 0.0f);
            transform.localScale = new Vector2(Mathf.Sign(-1), 1f);
           
        }
        else
        {
            rbd2.velocity = new Vector2(5, 0.0f);
            transform.localScale = new Vector2(Mathf.Sign(1), 1f);
            
        }
        if (Mathf.Abs(spearAttack.position.x)>(inicial +disMax) || Mathf.Abs(spearAttack.position.x) < (inicial - disMax))
        {
            Destroy(this.gameObject);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Spear>().enabled = false;
        }
    }
    public void TakeDemage(int damage)
    {
        Destroy(this.gameObject);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Spear>().enabled = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spearAttack.position, attackRange);
    }
}

