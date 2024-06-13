using System.Collections;
using System.Collections.Generic;
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
        return (InitialPosition) + (InitialVelocity * Time) + (0.5f * gravity * Mathf.Pow(Time, 2));
    }
   
}
