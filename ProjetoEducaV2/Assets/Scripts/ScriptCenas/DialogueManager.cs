using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    [TextArea(3,15)]
    public string[] lines;
    public float typingSpeed = 0.05f;
    public int index;

    //private int currentSentenceIndex;
    public bool isTyping;
    public bool isComplete;
    private bool dialogueStarted;
   // PlayerOne playerOne;

    private void Start()
    {
        //playerOne = FindObjectOfType<PlayerOne>();
        dialogueText.text = string.Empty;
        dialogueStarted = false;
    }

    private void Update()
    {
        // Verifica se o jogador tocou brevemente na tela após interagir com o NPC
        if (dialogueStarted && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (isTyping)
            {
                // Se estiver digitando, exibe o texto completo
                CompleteSentence();
            }
            else if (isComplete)
            {
                // Se o texto estiver completo, avança para a próxima sentença
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue()
    {
        if (!dialogueStarted)
        {
            
            dialogueStarted = true;
            index = 0;
            StartCoroutine(TypeLine());
        }
    }

    public void DisplayNextSentence()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
           dialogueStarted = false;
           dialogueText.text = string.Empty;
           gameObject.SetActive(false);
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        isComplete = false;
        dialogueText.text = string.Empty;

        yield return new WaitForSeconds(0.1f);

        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
       
        isTyping = false;
        isComplete = true;
      
    }

    void CompleteSentence()
    {
        StopAllCoroutines();
        dialogueText.text = lines[index];
        isTyping = false;
        isComplete = true;
    }

   
}
