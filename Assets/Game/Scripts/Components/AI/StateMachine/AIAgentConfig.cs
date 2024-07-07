using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AIAgentConfig : ScriptableObject
{
    public float MaxTime = 1.0f;
    public float MaxDistance = 1.0f;
    public float MaxSightDistance = 5.0f;
}
