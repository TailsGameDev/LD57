using UnityEngine;

public class AntKiller : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ant")
        {
            Destroy(other.gameObject);
        }
    }
}
