using UnityEngine;
using System.Collections;
public class Shaker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 PositionScale = new Vector3(1, 1, 0);
    public Vector3 RotationScale = new Vector3(0, 0, 0);
    [Range(1, 50)]
    public int shakeSpeed = 1;
    public SpriteRenderer sr;

    Vector3 origin;
    Quaternion originRotation;
    Coroutine co = null;

    /// <summary>
    /// Shake at POWER for DURATION seconds. Multiple calls will yield most recent only.
    /// </summary>
    /// <param name="duration">Duration of shaking</param>
    /// <param name="power">Optional. How violent to shake (0-50). Defaults to previous used Power.</param>
    public void Shake(float duration, int power = 0)
    {
        if (co != null)
        {
            StopCoroutine(co);
            RevertOrigin();
        }
        shakeSpeed = power > 0 ? power : shakeSpeed;
        co = StartCoroutine(DoShake(duration));
    }

    /// <summary>
    /// Set current position and rotation as 'origin'
    /// </summary>
    void SetOrigin()
    {
        sr.color = Color.red;
        origin = transform.localPosition;
        originRotation = transform.localRotation;
    }

    /// <summary>
    /// Revert the transform to the original position and rotation
    /// </summary>
    void RevertOrigin()
    {
        sr.color = Color.white;
        transform.localPosition = origin;
        transform.localRotation = originRotation;
        co = null;
    }

    IEnumerator DoShake(float duration)
    {
        SetOrigin();
        float speed = shakeSpeed / 200f;
        // Start on a 0.5f value (center) for smooth transition into shaking.
        Vector2 noise = new Vector2(Random.value * 10, Random.value * 100);
        float factor = Mathf.PerlinNoise(noise.x, noise.y);
        while (factor > 0.525f || factor < 0.475f)
        {
            noise.x += 0.05f;
            factor = Mathf.PerlinNoise(noise.x, noise.y);
        }
        // Shake for DURATION
        Vector3 offset = origin;
        float step = 0;
        float elapsed = 0;
        while (elapsed < duration)
        {
            // Generate unique perlin noise, only if needed
            if (PositionScale.x != 0)
                offset.x = (Mathf.PerlinNoise(noise.x + step, noise.y) * 2f - 1f) * PositionScale.x;
            if (PositionScale.y != 0)
                offset.y = (Mathf.PerlinNoise(noise.x + step * 0.41f, noise.y + step * 0.41f) * 2f - 1f) * PositionScale.y;
            // Update transform
            transform.localPosition = origin + offset;
            step += speed;
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Smoothly return to start (this bleeds over the given 'duration')
        float dt = 0;
        Vector3 at = transform.localPosition;
        Quaternion rot = transform.localRotation;
        while (dt < 1)
        {
            transform.localPosition = Vector3.Lerp(at, origin, dt);
            dt += shakeSpeed / 50f;
            yield return null;
        }
        RevertOrigin();
    }

}
