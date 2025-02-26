using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public string[] dialogueLines;
    public string[] names;

    private int lineIndex = 0;
    private int nameIndex = 0;
    private int letterIndex = 0;

    private string displayText = null;

    private float timePerChar = 0.02f;
    private float timer = 0;

    public GameObject player;
    public GameObject interlocutor;

   public void Start()
    {
        nameIndex = 0;
        nameText.text = names[nameIndex];
        lineIndex = 0;
        VerifyWhoIsTalking();
    }

   public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 )
        {
            timer += timePerChar;
            TypewrittingEffect();
        }

        if (Input.GetButton($"Interact"))
        {
            if (displayText != dialogueLines[lineIndex])
            {
                timer = 9999;
                displayText = dialogueLines[lineIndex];
                dialogueText.text = displayText;
            }
            else if (lineIndex < dialogueLines.Length - 1)
            {
                lineIndex++;
                nameIndex++;
                nameText.text = names[nameIndex];
                letterIndex = 0;
                timer = 0;
                VerifyWhoIsTalking();
            }
            else
            {
                gameObject.SetActive(false);
                player.gameObject.GetComponent<PlayerInteraction>().isInteracting = false;
                if (interlocutor.CompareTag("Ghost"))
                {
                    interlocutor.GetComponent<Ghost>().animator.SetBool("isInDialogue", false);
                    interlocutor.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }

    private void TypewrittingEffect()
    {
        if (letterIndex < dialogueLines[lineIndex].Length)
        {
            displayText += dialogueLines[lineIndex][letterIndex];
            dialogueText.text = displayText;
            letterIndex++;
        }

        timer = 0;
    }

    private void VerifyWhoIsTalking()
    {
        if (names[nameIndex] == "Sally Face")
        {
            player.GetComponent<PlayerInteraction>().isTalking = true;
            if (interlocutor.CompareTag("Ghost"))
            {
                interlocutor.gameObject.GetComponent<Ghost>().animator.SetBool("isTalking", false);
            }
        }
        else
        {
            player.GetComponent<PlayerInteraction>().isTalking = false;
            if (interlocutor.CompareTag("Ghost"))
            {
                interlocutor.gameObject.GetComponent<Ghost>().animator.SetBool("isTalking", true);
            }
        }
    }
}
