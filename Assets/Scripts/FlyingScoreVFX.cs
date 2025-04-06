using System;
using TMPro;
using UnityEngine;

public class FlyingScoreVFX : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amountText = null;
    [SerializeField]
    private float speed = 0.0f;
    
    private Transform targetPosition;
    private Action onComplete;

    public void Initialize(int amount, Transform targetPositionParam, Action onCompleteParam)
    {
        amountText.text = amount.ToString();

        targetPosition = targetPositionParam;
        
        onComplete = onCompleteParam;
    }

    private void Update()
    {
        Vector3.MoveTowards(current: transform.position, target: targetPosition.position,
                            maxDistanceDelta: speed * Time.deltaTime);
        
        // If distance is lesser than tolerance
        const float TOLERANCE = 0.1f;
        if ((targetPosition.position - transform.position).sqrMagnitude < TOLERANCE)
        {
            onComplete?.Invoke();
            Destroy(gameObject);
        }
    }
}
