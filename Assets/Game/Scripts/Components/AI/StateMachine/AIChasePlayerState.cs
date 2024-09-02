using UnityEngine;

public class AIChasePlayerState : AIState
{
    private float _timer = 0.0f;

    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;
    }
    public void Enter(AIAgent agent)
    {
        _timer = 0.0f;
        agent.NavMeshAgent.destination = agent.TargetEnemy.transform.position;
        agent.NavMeshAgent.speed = 4.0f;
        agent.NavMeshAgent.stoppingDistance = 6.5f;
    }

    public void Exit(AIAgent agent)
    {
        _timer = 0.0f;
        agent.NavMeshAgent.stoppingDistance = 0.0f;
    }

    public void Update(AIAgent agent)
    {
        if (!agent.enabled)
            return;
        _timer += Time.deltaTime;
        agent.NavMeshAgent.destination = agent.TargetEnemy.transform.position;
        if (_timer > 2.0f)
        {
            if (agent.NavMeshAgent.remainingDistance != float.PositiveInfinity && agent.NavMeshAgent.remainingDistance <= agent.NavMeshAgent.stoppingDistance)
            {
                agent.StateMachine.ChangeState(AIStateID.AttackPlayer);
            }
        }
    }

}
