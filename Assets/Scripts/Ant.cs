using UnityEngine;

public class Ant : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2D = null;
    [SerializeField]
    private float speed = 0.0f;

    private void Awake()
    {
        rb2D.AddForce(transform.right * speed);
    }
}
