using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationController : BaseAnimationComponent
{
    private readonly int SPEED = Animator.StringToHash("Speed");

    private AIAgent _agent;
    protected override void Awake()
    {
        _agent = GetComponent<AIAgent>();
        base.Awake();
    }
    private void Update()
    {
        SetSpeed(_agent.NavMeshAgent.velocity.magnitude);
    }
    public void SetSpeed(float speed)
    {
        Animator.SetFloat(SPEED, speed);
    }
   
}
