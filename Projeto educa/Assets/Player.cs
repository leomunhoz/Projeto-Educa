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
   
    bool isAttacking = false;

    public bool taNoChao;
    public Transform detectaChao;
    public LayerMask chao;
    string[] Anim = {"Idle","Run","Jump","Fall","Attack" };

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

        #region anim
        if (rb2d.velocity.x > 0)
        {
            transform.localScale = dir;
        }
        else if (rb2d.velocity.x < 0)
        {
            transform.localScale = esq;
        }

        if (rb2d.velocity.y > 0)
        {
            AnimMovement("Jump");
        }
        else if (rb2d.velocity.y < 0)
        {
            AnimMovement("Jump");
        }
        else if (rb2d.velocity.x > 0)
        {
            AnimMovement("Run");
        }
        else if (rb2d.velocity.x < 0)
        {
            AnimMovement("Run");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            AnimMovement("Attack");
            isAttacking = true;
        }
        else
        {
            AnimMovement("Idle");
        }

        #endregion









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

    void AnimMovement(string animCond) 
    {
        foreach (var item in Anim)
        {
            anim.SetBool(item, false);

        }
        anim.SetBool(animCond, true);
    }
}
        
       
       


       
   