using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject commandsPanel;
    public AudioSource audioSource;

    public AudioClip clickSound;
    public AudioClip hoverSound;

    public Animator animator;

    public string sceneToLoad;

    void Start()
    {
        animator.enabled = false;
    }

    public void ClickCommandsButton(TextMeshProUGUI text)
    {
        audioSource.clip = clickSound;
        audioSource.Play();
        mainPanel.SetActive(false);
        commandsPanel.SetActive(true);
        text.color = new Color32(255, 255, 255, 255);
    }

    public void ClickBackButton()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
        mainPanel.SetActive(true);
        commandsPanel.SetActive(false);
    }

    public void ClickQuitButton()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
        Application.Quit();
    }

    public void ClickPlayButton()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
        animator.enabled = true;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnHoveredButton(TextMeshProUGUI text)
    {
        audioSource.clip = hoverSound;
        audioSource.Play();
        text.color = new Color32(42, 147, 222, 255);
    }

    public void OnHoveredButtonQuit(TextMeshProUGUI text)
    {
        audioSource.clip = hoverSound;
        audioSource.Play();
        text.color = new Color32(222, 42, 54, 255);
    }

    public void OnUnhoveredButton(TextMeshProUGUI text)
    {
        audioSource.clip = hoverSound;
        audioSource.Play();
        text.color = new Color32(255, 255, 255, 255);
    }
}
