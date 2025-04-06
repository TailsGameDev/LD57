using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class Tongue : MonoBehaviour, IAntCatcher
{
    public static Tongue instance;

    public Vector2 direction = Vector2.zero;
    public TongueSegment lastSegment;
    private bool isRollingBack = false;
    public float timeDif = 0.05f;
    private float nextUpdateTime = 0f;

    public Sprite up;
    public Sprite down;
    public Sprite right;
    public Sprite left;
    private SpriteRenderer sr;

    public List<Transform> ants = new List<Transform>();


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        instance = this;
    }
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
        else if (ShouldMove() && isFreeSpace())
        {
            nextUpdateTime = Time.time + timeDif;
            TongueManager.CreateSegment(transform.position);
            Move();
        }
    }

    private bool isFreeSpace()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Ground", "Tongue Body"));
        if (hit.collider != null)
        {
            if (hit.collider.tag.Equals("Wall") || hit.collider.tag.Equals("Tongue Segment")) 
                return false;
        }
        return true;
    }
        
    private void RollBack()
    {

        sr.sprite = PickSprite(lastSegment.direction);
        transform.position = lastSegment.transform.position;
        lastSegment.ants.ForEach(ant => CatchAnt(ant));

        if (lastSegment.previous != null) { 
            SetLastSegment(lastSegment.previous.GetComponent<TongueSegment>(), true);

        }
        else
        {
            SetLastSegment(null, true);
            foreach (Transform ant in ants)
            {

                ant.GetComponent<Ant>().GetEaten();   
            }

            ants.Clear();
            isRollingBack = false;
        }
    }

    private bool ShouldMove()
    {
        return (direction != Vector2.zero && (lastSegment == null || direction != -1 * lastSegment.direction)) && Time.time > nextUpdateTime;
    }
    public void SetLastSegment(TongueSegment segment, bool destroy)
    {
        if (destroy)
            TongueManager.DestroySegment(lastSegment.transform);

        lastSegment = segment;
    }
    private void Move()
    {
        sr.sprite = PickSprite(direction);
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0
        );
    }

    private Sprite PickSprite(Vector2 direction)
    {
        if (direction == Vector2.up)
            return up;
        if (direction == Vector2.down)
            return down;
        if(direction == Vector2.left)
            return left;
        return right;

    }

    public void CatchAnt(Transform antTransform)
    {
        antTransform.parent = transform;
        Debug.Log("Catch!!");
        ants.Add(antTransform);
    }
}
