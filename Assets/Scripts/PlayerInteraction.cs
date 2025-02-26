using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
            hasGearBoyEquipped = !hasGearBoyEquipped;
            ChangeControllerAnimator();
        }

        if (Input.GetButton("Interact"))
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
        interactionCanvas.SetActive(false);
        spriteGb.SetActive(false);
        isInteracting = true;
        interactionCanvas.SetActive(false);
        dialogueCanvas.SetActive(true);
        dialogueCanvas.GetComponent<DialogueSystem>().interlocutor = interactive;
    }
}
