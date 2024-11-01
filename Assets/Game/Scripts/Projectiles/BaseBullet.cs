using UnityEngine;

public class BaseBullet
{
    public float Time;
    public Vector3 InitialPosition;
    public Vector3 InitialVelocity;
    public TrailRenderer Tracer;
    public Vector3 GetPosition(Vector3 gravity)
    {
        // p + v*t + 0.5 * g * t * t
        return (InitialPosition) + (InitialVelocity * Time) + (gravity * (0.5f * Mathf.Pow(Time, 2)));
    }
   
}
