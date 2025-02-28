using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Animator animator;

    public string[] dialogueLines = null;
    public string[] names = null;

    public AudioSource audioSource;
    public AudioClip appearSound;
    public AudioClip disappearSound;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerInteraction>().hasGearBoyEquipped)
            {
                other.gameObject.GetComponent<PlayerInteraction>().audioSource.clip = other.gameObject.GetComponent<PlayerInteraction>().lightGB;
                other.gameObject.GetComponent<PlayerInteraction>().audioSource.loop = true;
                other.gameObject.GetComponent<PlayerInteraction>().audioSource.Play();
                other.gameObject.GetComponent<PlayerInteraction>().spriteGb.SetActive(true);
                other.gameObject.GetComponent<PlayerInteraction>().interactionText.SetText("???");
                other.gameObject.GetComponent<PlayerInteraction>().interactionCanvas.SetActive(true);
                other.gameObject.GetComponent<PlayerInteraction>().interactive = gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerInteraction>().hasGearBoyEquipped)
            {
                other.gameObject.GetComponent<PlayerInteraction>().audioSource.Stop();
                other.gameObject.GetComponent<PlayerInteraction>().spriteGb.SetActive(false);
                other.gameObject.GetComponent<PlayerInteraction>().interactionCanvas.SetActive(false);
                other.gameObject.GetComponent<PlayerInteraction>().interactive = null;
            }
        }
    }

    public void OnInteract()
    {
        animator.enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
