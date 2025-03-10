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

    //private float timePerChar = 0.02f;
    //private float timer = 0;

    public GameObject player;
    public GameObject interlocutor;

    private bool _canClick = true;

   public void Start()
    {
    }

   void OnEnable()
    {
        _canClick = false;
        nameIndex = 0;
        nameText.text = names[nameIndex];
        lineIndex = 0;
        displayText = dialogueLines[lineIndex];
        dialogueText.text = displayText;
        VerifyWhoIsTalking();
        StartCoroutine(CanClickDelay());
        player.GetComponent<PlayerInteraction>().gbCanvas.SetActive(true);
        player.GetComponent<PlayerInteraction>().audioSource.clip = player.GetComponent<PlayerInteraction>().useGB;
        player.GetComponent<PlayerInteraction>().audioSource.loop = false;
        player.GetComponent<PlayerInteraction>().audioSource.Play();
        if (interlocutor.CompareTag("Ghost"))
        {
            interlocutor.GetComponent<Ghost>().audioSource.clip = interlocutor.GetComponent<Ghost>().appearSound;
            interlocutor.GetComponent<Ghost>().audioSource.Play();
        }
    }

   public void Update()
    {

        if (Input.GetButton($"Interact"))
        {
            if (_canClick)
            {
                if (lineIndex < dialogueLines.Length - 1)
                {
                    lineIndex++;
                    nameIndex++;
                    displayText = dialogueLines[lineIndex];
                    nameText.text = names[nameIndex];
                    dialogueText.text = displayText;
                    //letterIndex = 0;
                    VerifyWhoIsTalking();
                }
                else
                {
                    VerifyWhoIsTalking();
                    gameObject.SetActive(false);
                    player.gameObject.GetComponent<PlayerInteraction>().isInteracting = false;
                    //letterIndex = 0;
                    displayText = null;
                    if (interlocutor.CompareTag("Ghost"))
                    {
                        interlocutor.GetComponent<Ghost>().audioSource.clip = interlocutor.GetComponent<Ghost>().disappearSound;
                        interlocutor.GetComponent<Ghost>().audioSource.Play();
                        interlocutor.GetComponent<Ghost>().animator.SetBool("isInDialogue", false);
                        interlocutor.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        player.GetComponent<PlayerInteraction>().musicSource.Play();
                    }
                }

                if (gameObject.activeInHierarchy)
                {
                    _canClick = false;
                    StartCoroutine(CanClickDelay());
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

    IEnumerator CanClickDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _canClick = true;
    }
}
