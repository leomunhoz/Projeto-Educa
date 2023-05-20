using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerOneWayPlatform : MonoBehaviour
{
    [SerializeField] public GameObject currentOneWayPlatform;
    [SerializeField]private CapsuleCollider2D capsuleCollider2D;
    public PlayerOne player;
    public bool isPlatformDownPressed;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        isPlatformDownPressed = Gamepad.current.buttonSouth.isPressed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            
           
            currentOneWayPlatform = null;
        }
    }

    
    public IEnumerator DisableCollision() 
    {
        if (isPlatformDownPressed )
        {
            CompositeCollider2D platformCollidier = currentOneWayPlatform.GetComponent<CompositeCollider2D>();
            //Physics2D.IgnoreCollision(capsuleCollider2D, platformCollidier);
            platformCollidier.isTrigger = true;
            yield return new WaitForSeconds(0.5f);
            platformCollidier.isTrigger = false;
            //Physics2D.IgnoreCollision(capsuleCollider2D, platformCollidier, false);
        }
        
    }

    
}
