using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour
{
    public GameObject canvas;
    public Animator animator;

    public AudioSource audioSource;
    public AudioClip openDoor;

    public GameObject playerDestination;

    void Start()
    {
        HideCanvas();
    }

    void HideCanvas()
    {
        transform.parent.gameObject.GetComponent<PlayerInteraction>().isInteracting = false;
        canvas.SetActive(false);
        animator.enabled = false;
    }

    public void ShowCanvas()
    {
        transform.parent.gameObject.GetComponent<PlayerInteraction>().isInteracting = true;
        canvas.SetActive(true);
        animator.enabled = true;
    }

    void TeleportPlayer()
    {
        transform.parent.gameObject.transform.position = playerDestination.transform.position;
    }
}
