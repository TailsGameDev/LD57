using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private float maxPlayerHP = 0.0f;


    private int score;
    private float playerHP;


    public int Score { get => score; set => score = value; }
    public float PlayerHP { get => playerHP; set => playerHP = value; }
    public float MaxPlayerHP { get => maxPlayerHP; }


    private void Awake()
    {
        playerHP = maxPlayerHP;
    }
}
