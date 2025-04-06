using System.Collections.Generic;
using UnityEngine;

public class TongueSegment : MonoBehaviour, IAntCatcher
{
    public Vector2 direction;
    public SpriteRenderer spriteRenderer;
    public Transform previous;
    
    public List<Transform> ants = new List<Transform>();
    
    public Sprite vertical;
    public Sprite horizontal;
    public Sprite leftDown;
    public Sprite rightDown;
    public Sprite leftUp;
    public Sprite rightUp;




    public void SetupSegment(Tongue tongue)
    {
        direction = tongue.direction;
        if (tongue.lastSegment != null) previous = tongue.lastSegment.transform;
        spriteRenderer.sprite = PickSprite(tongue.direction);
        tongue.SetLastSegment(this, false);
    }

    public Sprite PickSprite(Vector2 target)
    {
        Vector2 origin = previous == null ? Vector2.down : previous.GetComponent<TongueSegment>().direction;

        if ((origin == Vector2.down && target == Vector2.down) || (origin == Vector2.up && target == Vector2.up))
        {
            return vertical;
        }
        if ((origin == Vector2.up && target == Vector2.left ) || (origin == Vector2.right && target == Vector2.down))
        {
            return leftDown;
        }
        if ((origin == Vector2.up && target == Vector2.right) || (origin == Vector2.left && target == Vector2.down))
        {
            return rightDown;
        }
        if ((origin == Vector2.down && target == Vector2.left) || (origin == Vector2.right && target == Vector2.up))
        {
            return leftUp;
        }
        if ((origin == Vector2.down && target == Vector2.right) || (origin == Vector2.left && target == Vector2.up))
        {
            return rightUp;
        }
        return horizontal;
    }

    public void CatchAnt(Transform antTransform)
    {
        antTransform.parent = transform;
        Debug.Log("Catch!!");
        ants.Add(antTransform);

    }
}
