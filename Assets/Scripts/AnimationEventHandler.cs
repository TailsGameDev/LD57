using System;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private Action onAttackAnimationFrame;
    public void Initialize(Action onAttackAnimationFrameParam)
    {
        onAttackAnimationFrame = onAttackAnimationFrameParam;
    }

    public void OnAttackAnimationFrame()
    {
        onAttackAnimationFrame?.Invoke();
    }
}
