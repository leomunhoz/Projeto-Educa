using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    private float direction;

    public bool taNoChao;
    public Transform detectaChao;
    public LayerMask chao;

    public int pulosExtras = 1;
    public int axPulosExtras;

    void Start()
    {
        axPulosExtras = pulosExtras;
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.3f, chao);

        direction = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);

        //characterController

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