using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText = null;

    private static UIManager instance;

    public static UIManager Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }

    public void SetScore(int score)
    {
        scoreText.text = "score: " + score;
    }
}
