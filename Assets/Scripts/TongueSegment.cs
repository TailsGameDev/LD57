using UnityEngine;

public class TongueSegment : MonoBehaviour 
{
    public Vector2 direction;
    public Transform previous;


    public void SetupSegment(Tongue tongue)
    {
        direction = tongue.direction;
        if(tongue.lastSegment != null) previous = tongue.lastSegment.transform;
        tongue.SetLastSegment(this, false);
    }
}
