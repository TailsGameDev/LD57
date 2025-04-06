using UnityEngine;

public class SoldierAnt : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2D = null;
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private int score = 0;

    [SerializeField]
    private float damage = 0.0f;
    [SerializeField]
    private float timeIntervalToAttack = 0.0f;
    [SerializeField]
    private float timeIntervalToAnimateIdle = 0.0f;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;
    [SerializeField]
    private Sprite idleSprite = null;
    [SerializeField]
    private Sprite attackSprite = null;
    [SerializeField]
    private float minSqrDistanceToTongueTip = 0.0f;

    private bool isAttacking;
    private float timeToAttack;
    private float timeToAnimateIdle;

    private void Awake()
    {
        rb2D.AddForce(transform.right * speed);
    }

    private void Update()
    {
        if (isAttacking)
        {
            float sqrDistanceToTongueTip = (Tongue.instance.transform.position - transform.position).sqrMagnitude;
            if (minSqrDistanceToTongueTip > sqrDistanceToTongueTip)
            {
                DieAndScorePoints();
            }
            else if (timeToAttack < Time.time)
            {
                // Update player hp in state and UI
                float currentHP = GameState.Instance.PlayerHP;
                float newHP = currentHP - damage;
                GameState.Instance.PlayerHP = newHP;
                UIManager.Instance.SetPlayerHP(newHP);

                // Change sprite like an attack animation
                spriteRenderer.sprite = attackSprite;

                // Set timers
                timeToAttack = Time.time + timeIntervalToAttack;
                timeToAnimateIdle = Time.time + timeIntervalToAnimateIdle;
            }
            else if (Time.time > timeToAnimateIdle)
            {
                // Set to idle, like the attack animation is ending
                spriteRenderer.sprite = idleSprite;

                // Set time to animate idle to infinity so it gets the real time once the ant attacks again
                timeToAnimateIdle = float.MaxValue;
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        string otherTag = collision2D.collider.tag;
        if (otherTag == "Tongue Tip")
        {
            DieAndScorePoints();
        }
        else if (otherTag == "Tongue Segment")
        {
            rb2D.linearVelocity = Vector2.zero;
            isAttacking = true;
        }
    }

    private void DieAndScorePoints()
    {
        // Set new score on game state and UI
        int currentScore = GameState.Instance.Score;
        int newScore = currentScore + score;
        GameState.Instance.Score = newScore;
        UIManager.Instance.SetScore(newScore);

        Destroy(gameObject);
    }
}
