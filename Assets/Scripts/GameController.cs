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
    private float targetHunger;

    private float hungerTimer = 0f;
    [SerializeField] 
    private float hungerDecayInterval = 1f;
    [SerializeField] 
    private float baseHungerDecay = 3f;
    [SerializeField]
    private float incrementHungerDecay = 1f;

    [SerializeField]
    private float eatingRegenHungerFactor=0.1f;



    public static GameController Instance { get => instance; }


    private void Awake()
    {
        Time.timeScale = 1.0f;
        instance = this;
        uiManager.Initialize(gameState.MaxPlayerHP,gameState.MaxPlayerHunger);
        targetHp = gameState.MaxPlayerHP;
        // Debug.Log($"[Init] MaxPlayerHunger: {gameState.MaxPlayerHunger} | PlayerHunger: {gameState.PlayerHunger}");
        uiManager.SetHighScore();

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

        if (!isGameOver && gameState.PlayerHunger <= 0)
        {
            GameOver();
        }


        if (gameState.PlayerHP != targetHp)
        {
            gameState.PlayerHP--;
            uiManager.SetPlayerHunger(gameState.PlayerHunger);

            // Verifica se a fome zerou
            if (gameState.PlayerHunger <= 0 && !isGameOver)
            {
                GameOver();
            }


            if ((gameState.PlayerHP <= 0))
            {
                GameOver();
            }
        }


        if (!isGameOver)
        {
            hungerTimer += Time.deltaTime;

            if (hungerTimer >= hungerDecayInterval)
            {
                hungerTimer = 0f;

                float timeFactor = Mathf.Min(Time.timeSinceLevelLoad / 60f, 3f); // Limita o fator no m�ximo em 3
                float decayAmount = baseHungerDecay + (timeFactor * incrementHungerDecay);

                gameState.PlayerHunger -= decayAmount;
                gameState.PlayerHunger = Mathf.Clamp(gameState.PlayerHunger, 0f, gameState.MaxPlayerHunger);

                uiManager.SetPlayerHunger(gameState.PlayerHunger);
            }
        }

        UpdateUI();

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
            HungerFeeding(amount);
        }
    }

    private void CheckAndSaveHighScore()
    {
        int currentScore = gameState.Score;
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save(); 
            // Debug.Log("New High Score: " + currentScore);
        }
    }


    public void HitPlayer(float damage)
    {
        targetHp = gameState.PlayerHP - damage;
    }
    public void HungerDecay(float decay)
    {
        targetHunger = gameState.PlayerHunger - decay;
    }
    public void HungerFeeding(float amount)
    {
        float hungerRegen = amount * eatingRegenHungerFactor; 
        gameState.PlayerHunger += hungerRegen;
        gameState.PlayerHunger = Mathf.Clamp(gameState.PlayerHunger, 0f, gameState.MaxPlayerHunger);

        uiManager.SetPlayerHunger(gameState.PlayerHunger);

    }
    public void GameOver()
    {
        isGameOver = true;
        CheckAndSaveHighScore();

        uiManager.ShowGameOverText();
        uiManager.SetHighScore();

        Time.timeScale = 0.0f;

        unscaledTimeToGoToMenu = Time.unscaledTime + gameOverStateDuration;
    }
    private void UpdateUI()
    {
        uiManager.SetPlayerHP(gameState.PlayerHP);
        uiManager.SetPlayerHunger(gameState.PlayerHunger);
    }

}
