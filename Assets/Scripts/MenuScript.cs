using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject creditsPanel;


    public void ShowCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowMainMenu()
    {
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }
}
