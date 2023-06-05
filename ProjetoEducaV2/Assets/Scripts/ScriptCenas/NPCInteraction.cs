using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    //public GameObject interactionButton;
    public GameObject dialogueBox;
    public GameObject interactionButton;
    public GameObject healingButton;
    public DialogueManager dialogueManager;
    public PlayerOne playerOne;
    private bool canInteract;
    Mapa1 mapa1;

    private void Start()
    {
        //interactionButton.SetActive(false);
        mapa1 = FindObjectOfType<Mapa1>();
        playerOne = mapa1.player.GetComponent<PlayerOne>();
       
    }

    private void Update()
    {
        if (canInteract && playerOne.isInteract)
        {
            dialogueBox.SetActive(true);
            dialogueManager.StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionButton.SetActive(true);
            healingButton.SetActive(false);
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionButton.SetActive(false);
            healingButton.SetActive(true);
            dialogueBox.SetActive(false);
            canInteract = false;
        }
    }
}


