using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameState gameState = null;
    [SerializeField]
    private UIManager uiManager = null;
    [SerializeField]
    private float gameOverStateDuration = 0.0f;


    private bool isGameOver;
    private bool isLoadingMainMenu;
    private float unscaledTimeToGoToMenu;
    private static GameController instance;


    public static GameController Instance { get => instance; }


    private void Awake()
    {
        Time.timeScale = 1.0f;
        instance = this;
        uiManager.Initialize(gameState.MaxPlayerHP);
    }
    private void Update()
    {
        bool isTimeToGoToMainMenu = isGameOver && (Time.unscaledTime > unscaledTimeToGoToMenu) && (!isLoadingMainMenu);
        if (isTimeToGoToMainMenu)
        {
            // Load main menu scene
            uiManager.ShowLoadingPanel();
            SceneManager.LoadSceneAsync("MainMenu");
            isLoadingMainMenu = true;
        }
    }

    public void IncreaseScore(int amount)
    {
        // Set new score on game state and UI
        int currentScore = gameState.Score;
        int newScore = currentScore + amount;
        gameState.Score = newScore;
        uiManager.SetScore(newScore);
    }

    public void HitPlayer(float damage)
    {
        // Update player hp in state and UI
        float currentHP = gameState.PlayerHP;
        float newHP = currentHP - damage;
        gameState.PlayerHP = newHP;
        uiManager.SetPlayerHP(newHP);

        // Treat game over situation
        if (newHP <= 0)
        {
            isGameOver = true;
            
            uiManager.ShowGameOverText();

            Time.timeScale = 0.0f;

            unscaledTimeToGoToMenu = Time.unscaledTime + gameOverStateDuration;
        }
    }
}
