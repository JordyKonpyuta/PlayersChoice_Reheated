using UnityEngine;

public class Door : MonoBehaviour
{
    public string destinationName;
    public GameObject destination;
    public GameObject canvasTransition;
    public Animator animator;

    public GameObject player;

    private string[] defaultDialogue = {"Y a personne."};
    private string[] defaultName = {"Sally Face"};

    void Start()
    {
        HideCanvas();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<PlayerInteraction>().hasGearBoyEquipped)
            {
                player = other.gameObject;
                other.gameObject.GetComponent<PlayerInteraction>().interactionText.SetText(destinationName);
                other.gameObject.GetComponent<PlayerInteraction>().interactionCanvas.SetActive(true);
                other.gameObject.GetComponent<PlayerInteraction>().interactive = gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<PlayerInteraction>().hasGearBoyEquipped)
            {
                other.gameObject.GetComponent<PlayerInteraction>().interactionCanvas.SetActive(false);
                other.gameObject.GetComponent<PlayerInteraction>().interactive = null;
            }
        }
    }

    public void OnInteract()
    {
        Debug.Log("coucou");
        if (destination != null) 
        { 
            if (destination.CompareTag("Door"))
            {
                canvasTransition.SetActive(true);
                animator.enabled = true;
            }

            else
            {
                player.gameObject.GetComponent<PlayerInteraction>().EnterInDialogue();
                player.gameObject.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().dialogueLines = defaultDialogue;
                player.gameObject.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().names = defaultName;
            }
        }

        else
        {
            player.gameObject.GetComponent<PlayerInteraction>().EnterInDialogue();
            player.gameObject.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().dialogueLines = defaultDialogue;
            player.gameObject.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().names = defaultName;
        }
    }

    public void TeleportPlayer()
    {
        player.transform.position = destination.transform.position;
    }

    public void HideCanvas()
    {
        canvasTransition.SetActive(false);
        animator.enabled = false;
        player = null;
    }
}
