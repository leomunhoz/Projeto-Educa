using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private Vector3 esq;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
        dir = transform.localScale;
        esq = transform.localScale;
        esq.x = esq.x * -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            anim.SetInteger("state", 1);
            transform.localScale = dir;

        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            anim.SetInteger("state", 1);
            transform.localScale = esq ;
        }
        else
        {
            anim.SetInteger("state", 0);
        }

        
        if (Input.GetAxis("Jump") > 0 )
        {
            anim.SetInteger("state", 2);
          
        }
        
    }
}
