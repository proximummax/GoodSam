using System.Collections;
using System.Collections.Generic;
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
    }

    public void Update(AIAgent agent)
    {
        agent.NavMeshAgent.destination = agent.MainPlayer.transform.position;
        UpdateFiring(agent);
        if (agent.MainPlayer.GetComponent<HealthComponent>().IsDead())
        {
            agent.StateMachine.ChangeState(AIStateID.Idle);
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
