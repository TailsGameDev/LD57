using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private float maxPlayerHP = 0.0f;
    [SerializeField]
    private float maxPlayerHunger = 0.0f;


    private int score;
    private float playerHP;
    private float playerHunger;


    public int Score { get => score; set => score = value; }
    public float PlayerHP { get => playerHP; set => playerHP = value; }
    public float MaxPlayerHP { get => maxPlayerHP; }
    public float PlayerHunger { get => playerHunger;set => playerHunger = value; }
    public float MaxPlayerHunger { get => maxPlayerHunger; set => maxPlayerHunger = value; }


    private void Awake()
    {
        playerHP = maxPlayerHP;
        playerHunger= maxPlayerHunger;
    }
}
