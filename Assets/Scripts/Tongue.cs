using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Tongue : MonoBehaviour
{

    public Vector2 direction = Vector2.zero;
    public TongueSegment lastSegment;
    private bool isRollingBack = false;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("left");
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("down");
            direction = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("right");
            direction = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("up");
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && TongueManager.HasTongue())
        {
            isRollingBack = true;
        }
        else direction = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (isRollingBack) RollBack();
        else if (ShouldMove())
        {
            TongueManager.CreateSegment(transform.position);
            Move();
        }
    }

    private void RollBack()
    {


        transform.position = lastSegment.transform.position;
        if (lastSegment.previous != null)
            SetLastSegment(lastSegment.previous.GetComponent<TongueSegment>(), true);
        else
        {
            SetLastSegment(null, true);
            isRollingBack = false;
        }
    }

    private bool ShouldMove()
    {
        return direction != Vector2.zero && (lastSegment == null || direction != -1 * lastSegment.direction);
    }
    public void SetLastSegment(TongueSegment segment, bool destroy)
    {
        if (destroy)
            TongueManager.DestroySegment(lastSegment.transform);

        lastSegment = segment;
    }
    private void Move()
    {
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0
        );
    }
}
