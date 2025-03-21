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

    private WaitForSeconds _skipDelay = new WaitForSeconds(0.02f);

    public GameObject player;
    public GameObject interlocutor;

    private bool _canClick = true;

   void OnEnable()
    {
        _canClick = false;
        nameIndex = 0;
        nameText.text = names[nameIndex];
        lineIndex = 0;
        dialogueText.text = null;
        StartCoroutine(TypewrittingEffect());
        VerifyWhoIsTalking();
        StartCoroutine(CanClickDelay());
        if (interlocutor.CompareTag("Ghost"))
        {
            interlocutor.GetComponent<Ghost>().audioSource.clip = interlocutor.GetComponent<Ghost>().appearSound;
            interlocutor.GetComponent<Ghost>().audioSource.Play();
            interlocutor.gameObject.GetComponent<Ghost>().isInDialogue = true;
            player.GetComponent<PlayerInteraction>().StopCameraAnimation();
        }
    }

   public void Update()
    {

        if (Input.GetButton($"Interact"))
        {
            if (_canClick)
            {
                if (dialogueText.text != dialogueLines[lineIndex])
                {
                    SkipDialogue();
                }

                else if (lineIndex < dialogueLines.Length - 1)
                {
                    dialogueText.text = null;
                    lineIndex++;
                    nameIndex++;
                    nameText.text = names[nameIndex];
                    StartCoroutine(TypewrittingEffect());
                    VerifyWhoIsTalking();
                }
                else
                {
                    gameObject.SetActive(false);
                    player.gameObject.GetComponent<PlayerInteraction>().isInteracting = false;
                    if (interlocutor.CompareTag("Ghost"))
                    {
                        interlocutor.GetComponent<Ghost>().audioSource.clip = interlocutor.GetComponent<Ghost>().disappearSound;
                        interlocutor.GetComponent<Ghost>().audioSource.Play();
                        interlocutor.GetComponent<Ghost>().isInDialogue = false;
                        interlocutor.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        player.GetComponent<PlayerInteraction>().musicSource.Play();
                    }
                    else
                    {
                        interlocutor.GetComponent<NPC>().isTalking = false;
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

    IEnumerator TypewrittingEffect()
    {
        foreach (char c in dialogueLines[lineIndex])
        {
            if (dialogueText.text != dialogueLines[lineIndex])
            {
                dialogueText.text = dialogueText.text + c;
                yield return _skipDelay;
            }
        }
    }

    private void SkipDialogue()
    {
        dialogueText.text = dialogueLines[lineIndex];
        StopCoroutine(TypewrittingEffect());
    }

    private void VerifyWhoIsTalking()
    {
        if (names[nameIndex] == "Sally Face")
        {
            player.GetComponent<PlayerInteraction>().isTalking = true;
            dialogueText.color = new Color32(102, 169, 232, 255);

            if (interlocutor.CompareTag("Ghost"))
            {
                interlocutor.gameObject.GetComponent<Ghost>().isTalking = false;
            }
            else if (interlocutor.CompareTag("NPC"))
            {
                interlocutor.gameObject.GetComponent<NPC>().isTalking = false;
            }
        }
        else
        {
            player.GetComponent<PlayerInteraction>().isTalking = false;
            if (interlocutor.CompareTag("Ghost"))
            {
                interlocutor.gameObject.GetComponent<Ghost>().isTalking = true;
                dialogueText.color = new Color32(234, 20, 62, 255);
            }
            else if (interlocutor.CompareTag("NPC"))
            {
                interlocutor.gameObject.GetComponent<NPC>().isTalking = true;
                dialogueText.color = new Color32(255, 255, 255, 255);
            }
        }
    }

    IEnumerator CanClickDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _canClick = true;
    }
}
