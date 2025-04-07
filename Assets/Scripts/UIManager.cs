using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText = null;
    [SerializeField]
    private Slider playerHPSlider = null;
    [SerializeField]
    private TMPro.TextMeshProUGUI gameOverText = null;
    [SerializeField]
    private GameObject loadingPanel = null;
    [SerializeField]
    private FlyingScoreVFX flyingScoreVFX = null;
    [SerializeField]
    private RectTransform vfxParent = null;
    [SerializeField]
    private Image StomachFill = null;

    [SerializeField]
    private TMPro.TextMeshProUGUI highScoreText;


    private float timeToTryFixCameraAspectAgain;

    private float playerMaxHP;
    private float playerMaxHunger;



    private int lastScreenWidth;
    private int lastScreenHeight;


    private void Update()
    {
        if (timeToTryFixCameraAspectAgain < Time.time)
        {
            FixCameraAspect();

            // Set to max value so camera never gets fixed again
            timeToTryFixCameraAspectAgain = float.MaxValue;
        }

        // Call fix camera aspect whenever screen changes
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;

            FixCameraAspect();
        }
    }
    public void Initialize(float playerMaxHPParam, float playerMaxHungerParam)
    {
        playerMaxHP = playerMaxHPParam;
        playerMaxHunger = playerMaxHungerParam;

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;

        FixCameraAspect();

        timeToTryFixCameraAspectAgain = Time.time + 0.5f;
    }
    private void FixCameraAspect()
    {
        // Run IA code to scale screen to aspect 16:9
        const float TARGET_ASPECT = 16.0f / 9.0f;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / TARGET_ASPECT;
        if (scaleHeight < 1.0f) {
            Rect rect = Camera.main.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            Camera.main.rect = rect;
        } else {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = Camera.main.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            Camera.main.rect = rect;
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SpawnScoreVFX(Transform scoreVFXStartPosition, int increaseAmount, Action onComplete)
    {
        FlyingScoreVFX vfx = Instantiate(flyingScoreVFX, Camera.main.WorldToScreenPoint(scoreVFXStartPosition.position), Quaternion.identity);
        vfx.Initialize(increaseAmount, scoreText.transform, onComplete);
        vfx.transform.SetParent(vfxParent);
        vfx.transform.localScale = Vector3.one;
    }
    public Vector3 WorldToCanvasPosition(Vector3 worldPosition)
    {
        // Convert world position to screen position
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // Convert screen position to canvas local position
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(), 
            screenPosition, 
            Camera.main, 
            out canvasPosition
        );

        return canvasPosition;
    }


    public void SetPlayerHP(float playerHP)
    {
        playerHPSlider.value = (playerHP / playerMaxHP);
    }

    public void SetPlayerHunger(float playerHunger) 
    {
        StomachFill.fillAmount = (playerHunger/playerMaxHunger);


        Debug.Log($"[UI] SetPlayerHunger: {playerHunger} / {playerMaxHunger} = {playerHunger / playerMaxHunger}");

    }

    public void ShowGameOverText()
    {
        gameOverText.gameObject.SetActive(true);
    }

    public void ShowLoadingPanel()
    {
        loadingPanel.SetActive(true);
    }
    public void SetHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }

}
