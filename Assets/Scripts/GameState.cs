using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState instance;

    private int score;

    public static GameState Instance { get => instance; }
    public int Score { get => score; set => score = value; }

    private void Awake()
    {
        instance = this;
    }
}
