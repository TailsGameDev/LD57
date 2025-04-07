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
    private float targetHp;


    public static GameController Instance { get => instance; }


    private void Awake()
    {
        Time.timeScale = 1.0f;
        instance = this;
        uiManager.Initialize(gameState.MaxPlayerHP);
        targetHp = gameState.MaxPlayerHP;
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
        if(gameState.PlayerHP != targetHp)
        {
            gameState.PlayerHP--;
            uiManager.SetPlayerHP(gameState.PlayerHP);

            if (gameState.PlayerHP <= 0)
            {
                isGameOver = true;

                uiManager.ShowGameOverText();

                Time.timeScale = 0.0f;

                unscaledTimeToGoToMenu = Time.unscaledTime + gameOverStateDuration;
            }
        }
    }

    public void IncreaseScore(int amount, Transform scoreVFXStartPosition = null)
    {
        // Call animation to score if possible. Otherwise score immediately
        if (scoreVFXStartPosition != null)
        {
            uiManager.SpawnScoreVFX(scoreVFXStartPosition, amount, onComplete: Score);
        }
        else
        {
            Score();
        }
        void Score()
        {
            // Set new score on game state and UI
            int currentScore = gameState.Score;
            int newScore = currentScore + amount;
            gameState.Score = newScore;
            uiManager.SetScore(newScore);
        }
    }

    public void HitPlayer(float damage)
    {
        targetHp = gameState.PlayerHP - damage;
    }
}
