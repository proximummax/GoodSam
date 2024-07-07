using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationController : BaseAnimationComponent
{
    private readonly int SPEED = Animator.StringToHash("Speed");
    protected override void Awake()
    {
        base.Awake();
    }
    public void SetSpeed(float speed)
    {
        Animator.SetFloat(SPEED, speed);
    }
}
