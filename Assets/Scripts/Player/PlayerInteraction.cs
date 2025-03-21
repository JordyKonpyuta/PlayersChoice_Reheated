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

    public GameObject gbCanvas;
    public AudioSource audioSource;

    public AudioSource musicSource;

    public AudioClip equipGB;
    public AudioClip useGB;
    public AudioClip lightGB;

    public GameObject dialogueCanvas;

    private static readonly int IsTalking = Animator.StringToHash("IsTalking");
    public Animator animator;
    public RuntimeAnimatorController normal;
    public RuntimeAnimatorController gbEquipped;

    public Animator cameraAnimator;
    private static readonly int Interact_Right = Animator.StringToHash("Interact_Right");
    private static readonly int Interact_Left = Animator.StringToHash("Interact_Left");

    private bool _canClick = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionCanvas.SetActive(false);
        animator.runtimeAnimatorController = normal;
        spriteGb.SetActive(false);
        gbCanvas.SetActive(false);
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
                audioSource.clip = equipGB;
                audioSource.loop = false;
                audioSource.Play();
                StartCoroutine(CanClickDelay());
            }
            
        }

        if (Input.GetButton("Interact"))
        {
            if (_canClick )
            {
                if (interactive != null)
                {
                    dialogueCanvas.GetComponent<DialogueSystem>().interlocutor = interactive;

                    if (interactive.CompareTag("Ghost"))
                    {
                        dialogueCanvas.GetComponent<DialogueSystem>().dialogueLines = interactive.GetComponent<Ghost>().dialogueLines;
                        dialogueCanvas.GetComponent<DialogueSystem>().names = interactive.GetComponent<Ghost>().names;
                        if (hasGearBoyEquipped)
                        {
                            musicSource.Stop();
                            gbCanvas.SetActive(true);
                            audioSource.clip = useGB;
                            audioSource.loop = false;
                            audioSource.Play();
                            StartCoroutine(HideGBCanvas());
                            if (gameObject.GetComponent<SpriteRenderer>().flipX)
                            {
                                cameraAnimator.SetBool(Interact_Right, true);
                            }
                            else
                            {
                                cameraAnimator.SetBool(Interact_Right, false);
                            }
                            EnterInDialogue();
                            StartCoroutine(DisplayDialogueCanvas());
                        }
                    }

                    else if (interactive.CompareTag("NPC"))
                    {
                        dialogueCanvas.GetComponent<DialogueSystem>().dialogueLines = interactive.GetComponent<NPC>().dialogueLines;
                        dialogueCanvas.GetComponent<DialogueSystem>().names = interactive.GetComponent<NPC>().names;
                        EnterInDialogue();
                        dialogueCanvas.GetComponent<DialogueSystem>().interlocutor = interactive;
                        dialogueCanvas.SetActive(true);
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
    }

    void FixedUpdate()
    {
       animator.SetBool(IsTalking, isTalking);
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
    }

    IEnumerator CanClickDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _canClick = true;
    }

    IEnumerator HideGBCanvas()
    {
        yield return new WaitForSeconds(0.2f);
        gbCanvas.SetActive(false);
    }

    IEnumerator DisplayDialogueCanvas()
    {
        yield return new WaitForSeconds(2f);
        dialogueCanvas.SetActive(true);
    }

    public void InteractGhost()
    {
        interactive.GetComponent<Ghost>().OnInteract();
        interactive.GetComponent<Ghost>().audioSource.clip = interactive.GetComponent<Ghost>().appearSound;
        interactive.GetComponent<Ghost>().audioSource.Play();
        interactive.gameObject.GetComponent<Ghost>().isInDialogue = true;
    }

    public void StopCameraAnimation()
    {
        cameraAnimator.SetBool(Interact_Right, false);
        cameraAnimator.SetBool(Interact_Left, false);
    }
}
