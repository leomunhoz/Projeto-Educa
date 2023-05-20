using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public PlayerOne playerOne;
    public GameObject DialogueBox;
    public bool canTalk;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = FindObjectOfType<Dialogue>();
        playerOne = FindObjectOfType<PlayerOne>();
        DialogueBox = GameObject.FindGameObjectWithTag("Dig");
           
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOne.isInteract && canTalk)
        {
            DialogueBox.SetActive(true);
            dialogue.StartDialogue();
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collider");
        if (collision.gameObject.CompareTag("Player")) 
        {
             canTalk = true; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canTalk = false;
        }
    }
}
