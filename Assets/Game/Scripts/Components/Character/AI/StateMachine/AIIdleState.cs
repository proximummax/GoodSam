using UnityEngine;

public class AIIdleState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.Idle;
    }
    public void Enter(AIAgent agent)
    {
        agent.WeaponOwner.StopFire();
        agent.NavMeshAgent.ResetPath();
    }
    public void Update(AIAgent agent)
    {
        if (!agent.TargetEnemy.GetComponent<BaseHealthComponent>().IsDead())
        {
            Vector3 playerDirection = agent.TargetEnemy.transform.position - agent.transform.position;

            if (playerDirection.magnitude > agent.Config.MaxSightDistance)
                return;

            Vector3 agentDirection = agent.transform.forward;
            playerDirection.Normalize();

            float dotProduct = Vector3.Dot(playerDirection, agentDirection);
            
            if (dotProduct > 0.0f)
            {
                agent.StateMachine.ChangeState(AIStateID.ChasePlayer);
            }
        }
    }
    public void Exit(AIAgent agent)
    {

    }
    
}
