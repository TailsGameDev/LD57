using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameState gameState = null;
    [SerializeField]
    private UIManager uiManager = null;

    private static GameController instance;

    public static GameController Instance { get => instance; }

    private void Awake()
    {
        instance = this;
        uiManager.Initialize(gameState.MaxPlayerHP);
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
    }
}
