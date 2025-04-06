using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameState gameState = null;
    [SerializeField]
    private UIManager uiManager = null;

    private void Awake()
    {
        uiManager.Initialize(gameState.MaxPlayerHP);
    }
}
