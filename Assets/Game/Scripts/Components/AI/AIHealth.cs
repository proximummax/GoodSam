using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : HealthComponent
{
    private AIAgent _agent;
    protected override void OnStart()
    {
        _agent = GetComponent<AIAgent>();
    }
    protected override void OnDeath()
    {
        _agent.StateMachine.ChangeState(AIStateID.Death);
        _agent.NavMeshAgent.enabled = false;
    }
    protected override void OnDamage()
    {

    }
}
