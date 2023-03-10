using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Animator anim;
    Vector2 dir;
    Vector2 esq;
    string[] Anim = { "Idle", "Run", "Jump", "Fall", "Attack" };
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        dir = transform.localScale;
        esq = transform.localScale;
        esq.x = esq.x * -1;
    }

    // Update is called once per frame
    void Update()
    {
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
            AnimMovement("Fall");
        }
        else if (rb2d.velocity.x > 0)
        {
            AnimMovement("Run");
        }
        else if (rb2d.velocity.x < 0)
        {
            AnimMovement("Run");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AnimMovement("Attack");
            
        }
        else
        {
            AnimMovement("Idle");
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
