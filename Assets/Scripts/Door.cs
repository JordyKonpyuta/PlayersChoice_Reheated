using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public string destinationName;
    public GameObject destination;
    public GameObject player;

    public AudioSource audioSource;

    public AudioClip[] noDestinationSound;

    private string[] defaultDialogue = {"Y a personne."};
    private string[] defaultName = {"Sally Face"};

    void Start()
    {
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
        if (destination != null) 
        { 
            if (destination.CompareTag("Door"))
            {
                foreach (Transform child in player.transform)
                {
                    if (child.CompareTag("Door"))
                    {
                        child.GetComponent<DoorInteraction>().ShowCanvas();
                        child.GetComponent<DoorInteraction>().playerDestination = destination;
                    }
                }
            }

            else
            {
                audioSource.clip = noDestinationSound[Random.Range(0, noDestinationSound.Length)];
                audioSource.Play();
                player.GetComponent<PlayerInteraction>().EnterInDialogue();
                player.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().dialogueLines = defaultDialogue;
                player.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().names = defaultName;
            }
        }

        else
        {
            audioSource.clip = noDestinationSound[Random.Range(0, noDestinationSound.Length)];
            audioSource.Play();
            player.GetComponent<PlayerInteraction>().EnterInDialogue();
            player.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().dialogueLines = defaultDialogue;
            player.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().names = defaultName;
        }
    }
}
