using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public  Rigidbody2D rbd2;
    public  Collider2D[] playerHits;
    public  LayerMask playerLayer;
    public LayerMask Wall;
    public  Transform spearAttack;
    public  float attackRange;
    public int damage = 10;
    public static bool lado;
   
    public static void Direcao(bool direita)
    {
        lado = direita;
    }

    private void Awake()
    {
        rbd2 = GetComponent<Rigidbody2D>();
       
    }
    private void Update()
    {
        playerHits = Physics2D.OverlapCircleAll(spearAttack.position, attackRange, playerLayer);
        foreach (var Player in playerHits) 
        {
            print(damage);
            Player.GetComponent<PlayerOne>().TakeDamage(damage);
            Destroy(this.gameObject);
            //DestroyImmediate(this.gameObject);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Spear>().enabled = false;
        }
       
        if (!lado)
        {
            rbd2.velocity = new Vector2(-5, 0);
            transform.localScale = new Vector2(Mathf.Sign(-1), 1f);
           
        }
        else
        {
            rbd2.velocity = new Vector2(5, 0);
            transform.localScale = new Vector2(Mathf.Sign(1), 1f);
            
        }
        
       
        
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spearAttack.position, attackRange);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("SLA");
        if (collision.gameObject.layer == Wall )
        {
           
            Destroy(this.gameObject);
        }
       
    }

}

