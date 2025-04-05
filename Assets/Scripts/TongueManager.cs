using System.Collections.Generic;
using UnityEngine;

public class TongueManager : MonoBehaviour 
{
    private static TongueManager instance;
    public static TongueManager Instance { get => instance; } 


    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public Tongue tongue;

    public static Transform CreateSegment(Vector3 position)
    {
        Transform segmentTransform = Instantiate(instance.segmentPrefab, position, Quaternion.identity, instance.transform);
        segmentTransform.GetComponent<TongueSegment>().SetupSegment(instance.tongue);
        instance.segments.Add(segmentTransform);
        return segmentTransform;
    }

    public static void DestroySegment(Transform segmentTransform)
    {
        instance.segments.Remove(segmentTransform);
        GameObject.Destroy(segmentTransform.gameObject);

    }

    public static bool HasTongue()
    {
        return instance.segments.Count > 0;
    }
}
