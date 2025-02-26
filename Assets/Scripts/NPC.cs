using UnityEngine;

public class NPC : MonoBehaviour
{

    public Animator animator;

    public string[] dialogueLines;
    public string[] names;

    public string characterName;

    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<PlayerInteraction>().hasGearBoyEquipped)
            {
                other.gameObject.GetComponent<PlayerInteraction>().interactionText.SetText(characterName);
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

}
