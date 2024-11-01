public class AIAttackState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.AttackPlayer;
    }
    public void Enter(AIAgent agent)
    {
        agent.WeaponOwner.StartCoroutine(agent.WeaponOwner.ActivateWeapon());
        agent.WeaponOwner.SetTarget(agent.TargetEnemy.transform);
        agent.NavMeshAgent.stoppingDistance = 6.5f;
    }

    public void Exit(AIAgent agent)
    {
        agent.NavMeshAgent.stoppingDistance = 0.0f;
        agent.WeaponOwner.SetFiringState(false);
        agent.WeaponOwner.SetTarget(null);
    }

    public void Update(AIAgent agent)
    {
        agent.NavMeshAgent.destination = agent.TargetEnemy.transform.position;
      

        if (agent.NavMeshAgent.remainingDistance > agent.NavMeshAgent.stoppingDistance)
        {
            agent.StateMachine.ChangeState(AIStateID.ChasePlayer);
        }

        UpdateFiring(agent);

        if (agent.TargetEnemy.GetComponent<BaseHealthComponent>().IsDead())
        {
            agent.StateMachine.ChangeState(AIStateID.Idle);
        }
      
    }
    private void UpdateFiring(AIAgent agent)
    {
        agent.WeaponOwner.SetFiringState(agent.AISensor.IsInSight(agent.TargetEnemy.gameObject));
    }
}
