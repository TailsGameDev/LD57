using UnityEngine;

public class Ant : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2D = null;
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private int score = 0;

    private void Awake()
    {
        rb2D.AddForce(transform.right * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.tag == "Tongue Tip")
        {
            // Set new score on game state and UI
            int currentScore = GameState.Instance.Score;
            int newScore = currentScore + score;
            GameState.Instance.Score = newScore;
            UIManager.Instance.SetScore(newScore);

            Destroy(gameObject);
        }
    }
}
