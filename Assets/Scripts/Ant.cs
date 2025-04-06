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
        string otherTag = collision2D.collider.tag;
        if (otherTag == "Tongue Tip" || otherTag == "Tongue Segment")
        {
            GameController.Instance.IncreaseScore(score);
            Destroy(gameObject);
        }
    }
}
