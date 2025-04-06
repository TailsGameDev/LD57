using UnityEngine;

public class AntKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ant")
        {
            Destroy(other.gameObject);
        }
    }
}
