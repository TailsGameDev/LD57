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
    private Sprite idleSprite = null;
    [SerializeField]
    private Sprite attackSprite = null;
    [SerializeField]
    private float minSqrDistanceToTongueTip = 0.0f;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private AnimationEventHandler animationEventHandler = null;

    private bool isAttacking;

    private void Awake()
    {
        rb2D.AddForce(transform.right * speed);
        animationEventHandler.Initialize(onAttackAnimationFrameParam: OnAttackAnimationFrame);
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
        } 
    }
    private void OnAttackAnimationFrame()
    {
        GameController.Instance.HitPlayer(damage);
        Debug.LogError("hit here", this);
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
            animator.SetTrigger("Attack");
        }
    }

    private void DieAndScorePoints()
    {
        GameController.Instance.IncreaseScore(amount: score, transform);
        Destroy(gameObject);
    }
}
