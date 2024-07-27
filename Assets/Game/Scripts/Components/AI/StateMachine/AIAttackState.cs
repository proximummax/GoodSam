using UnityEngine;

public class AIAttackState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.AttackPlayer;
    }
    public void Enter(AIAgent agent)
    {
        agent.WeaponOwner.StartCoroutine(agent.WeaponOwner.ActivateWeapon());
        agent.WeaponOwner.SetTarget(agent.MainPlayer.transform);
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
        agent.NavMeshAgent.destination = agent.MainPlayer.transform.position;
        UpdateFiring(agent);

        if (agent.MainPlayer.GetComponent<HealthComponent>().IsDead())
        {
            agent.StateMachine.ChangeState(AIStateID.Idle);
        }

        if (agent.HealthComponent.IsLowHealth())
        {
          //  agent.StateMachine.ChangeState(AIStateID.FindHealth);
        }
        if (agent.WeaponOwner.GetActiveWeapon() != null && agent.WeaponOwner.GetActiveWeapon().IsAmmoEmpty())
        {
        //    agent.StateMachine.ChangeState(AIStateID.FindAmmo);
        }
    }
    private void UpdateFiring(AIAgent agent)
    {
        if (agent.AISensor.IsInSight(agent.MainPlayer.gameObject))
        {
            agent.WeaponOwner.SetFiringState(true);
        }
        else
        {
            agent.WeaponOwner.SetFiringState(false);
        }
    }
}
