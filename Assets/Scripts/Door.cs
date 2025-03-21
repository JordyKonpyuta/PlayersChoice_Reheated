using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public string destinationName;
    public GameObject destination;
    public GameObject player;

    public AudioSource audioSource;

    public AudioClip[] noDestinationSound;
    public AudioClip openDoor;

    public string[] defaultDialogue;
    public string[] defaultName;

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
                        player.GetComponent<PlayerInteraction>().audioSource.clip = openDoor;
                        player.GetComponent<PlayerInteraction>().audioSource.Play();
                    }
                }
            }
            else
            {
                if (!player.GetComponent<PlayerInteraction>().isTalking)
                {
                    audioSource.clip = noDestinationSound[Random.Range(0, noDestinationSound.Length)];
                    audioSource.Play();
                }
                player.GetComponent<PlayerInteraction>().EnterInDialogue();
                player.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().interlocutor = gameObject;
                player.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().dialogueLines = defaultDialogue;
                player.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().names = defaultName;
                player.GetComponent<PlayerInteraction>().dialogueCanvas.SetActive(true);
            }
        }


        else
        {
            audioSource.clip = noDestinationSound[Random.Range(0, noDestinationSound.Length)];
            audioSource.Play();
            player.GetComponent<PlayerInteraction>().EnterInDialogue();
            player.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().interlocutor = gameObject;
            player.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().dialogueLines = defaultDialogue;
            player.GetComponent<PlayerInteraction>().dialogueCanvas.GetComponent<DialogueSystem>().names = defaultName;
            player.GetComponent<PlayerInteraction>().dialogueCanvas.SetActive(true);
        }
    }
}