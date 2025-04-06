using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText = null;
    [SerializeField]
    private Slider playerHPSlider = null;

    private float playerMaxHP;

    public void Initialize(float playerMaxHPParam)
    {
        playerMaxHP = playerMaxHPParam;
    }

    public void SetScore(int score)
    {
        scoreText.text = "score: " + score;
    }

    public void SetPlayerHP(float playerHP)
    {
        playerHPSlider.value = (playerHP / playerMaxHP);
    }
}
