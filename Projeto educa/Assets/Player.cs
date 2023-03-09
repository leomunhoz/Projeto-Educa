using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    private float direction;
    Vector2 dir;
    Vector2 esq;

    public bool taNoChao;
    public Transform detectaChao;
    public LayerMask chao;

    public int pulosExtras = 0;
    public int axPulosExtras;

    public Animator anim;

    void Start()
    {
        axPulosExtras = pulosExtras;
        rb2d = GetComponent<Rigidbody2D>();
        anim.GetComponent<Animator>();
        dir = transform.localScale;
        esq = transform.localScale;
        esq.x = esq.x * -1;
    }


    void Update()
    {
        taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.3f, chao);

        direction = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
      
        Debug.Log(rb2d.velocity.y);
        if (rb2d.velocity.x > 0)
        {
            anim.Play("Run");
            transform.localScale = dir;
        }
        else if (rb2d.velocity.x < 0)
        {
            anim.Play("Run");
            transform.localScale = esq;
        }
        else
        {
            anim.Play("Idle");
        }





        if (Input.GetButtonDown("Jump") && taNoChao == true)
        {

            rb2d.velocity = Vector2.up * 6;
        }

        if (Input.GetButtonDown("Jump") && taNoChao == false && axPulosExtras > 0)
        {
            rb2d.velocity = Vector2.up * 6;
           
            axPulosExtras--;
            
        }

        if (taNoChao && axPulosExtras != pulosExtras)
        {
            axPulosExtras = pulosExtras;
        }
    }

}
        
       
       


       
   