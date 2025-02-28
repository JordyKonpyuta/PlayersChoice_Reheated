using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{

    public bool hasGearBoyEquipped = false;
    public GameObject spriteGb;
    public bool isTalking = false;
    public bool isInteracting = false;
    public GameObject interactive;

    public GameObject interactionCanvas;
    public TextMeshProUGUI interactionText;

    public GameObject dialogueCanvas;

    public Animator animator;
    public RuntimeAnimatorController normal;
    public RuntimeAnimatorController gbEquipped;

    private bool _canClick = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionCanvas.SetActive(false);
        animator.runtimeAnimatorController = normal;
        spriteGb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton($"EquipGB"))
        {
            if (_canClick ) 
            {
                hasGearBoyEquipped = !hasGearBoyEquipped;
                ChangeControllerAnimator();
                _canClick = false;
                StartCoroutine(CanClickDelay());
            }
            
        }

        if (Input.GetButton("Interact"))
        {
            if (_canClick )
            {
                if (interactive != null)
                {
                    if (interactive.CompareTag("Ghost"))
                    {
                        dialogueCanvas.GetComponent<DialogueSystem>().dialogueLines = interactive.GetComponent<Ghost>().dialogueLines;
                        dialogueCanvas.GetComponent<DialogueSystem>().names = interactive.GetComponent<Ghost>().names;
                        EnterInDialogue();
                        interactive.GetComponent<Ghost>().OnInteract();
                    }

                    else if (interactive.CompareTag("NPC"))
                    {
                        dialogueCanvas.GetComponent<DialogueSystem>().dialogueLines = interactive.GetComponent<NPC>().dialogueLines;
                        dialogueCanvas.GetComponent<DialogueSystem>().names = interactive.GetComponent<NPC>().names;
                        EnterInDialogue();
                    }

                    else if (interactive.CompareTag("Door"))
                    {
                        interactive.GetComponent<Door>().OnInteract();
                    }
                }
                _canClick = false;
                StartCoroutine(CanClickDelay());
            }
            
        }

        animator.SetBool("isTalking", this.isTalking);
    }

    void ChangeControllerAnimator()
    {
        if (hasGearBoyEquipped)
        {
            animator.runtimeAnimatorController = gbEquipped;
        }
        else animator.runtimeAnimatorController = normal;
    }

    public void EnterInDialogue()
    {
        hasGearBoyEquipped = false;
        ChangeControllerAnimator();
        spriteGb.SetActive(false);
        isInteracting = true;
        interactionCanvas.SetActive(false);
        dialogueCanvas.GetComponent<DialogueSystem>().interlocutor = interactive;
        dialogueCanvas.SetActive(true);
    }

    IEnumerator CanClickDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _canClick = true;
    }
}
