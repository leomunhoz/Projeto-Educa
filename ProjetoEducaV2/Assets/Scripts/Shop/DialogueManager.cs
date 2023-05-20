using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] sentences;
    public float typingSpeed = 0.05f;

    private int currentSentenceIndex;
    private bool isTyping;
    private bool isComplete;
    private bool dialogueStarted;

    private void Start()
    {
        dialogueText.text = "";
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
            currentSentenceIndex = 0;
            StartCoroutine(TypeSentence(sentences[currentSentenceIndex]));
        }
    }

    public void DisplayNextSentence()
    {
        if (currentSentenceIndex < sentences.Length - 1)
        {
            currentSentenceIndex++;
            dialogueText.text = "";
            StartCoroutine(TypeSentence(sentences[currentSentenceIndex]));
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        isComplete = false;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
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
        dialogueText.text = sentences[currentSentenceIndex];
        isTyping = false;
        isComplete = true;
    }

    void EndDialogue()
    {
        // Faça o que você precisar aqui, como desativar a caixa de diálogo ou iniciar uma ação do NPC.
        // Por exemplo:
        // dialogueBox.SetActive(false);
    }
}
