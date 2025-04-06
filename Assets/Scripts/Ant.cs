using Unity.VisualScripting;
using UnityEngine;

public class Ant : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2D = null;
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    public int score = 0;


    private void Awake()
    {
        rb2D.AddForce(transform.right * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        string otherTag = collision2D.collider.tag;
        if (otherTag == "Tongue Tip" || otherTag == "Tongue Segment")
        {
            var catcher = collision2D.collider.GetComponent<IAntCatcher>();
            if (catcher != null)
            {
                catcher.CatchAnt(this.transform);
                rb2D.linearVelocity = Vector3.zero;
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }

    public void GetEaten()
    {
        GameController.Instance.IncreaseScore(score, transform);
        Destroy(gameObject);
    }
}
