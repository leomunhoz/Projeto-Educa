using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionButton;
    public GameObject dialogueBox;
    public DialogueManager dialogueManager;

    private bool canInteract;

    private void Start()
    {
        interactionButton.SetActive(false);
    }

    private void Update()
    {
        if (canInteract && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionButton.SetActive(false);
            canInteract = false;
        }
    }
}


