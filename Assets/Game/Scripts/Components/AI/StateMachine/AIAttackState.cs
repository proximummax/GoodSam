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
        Debug.Log("come one?");
        agent.WeaponOwner.StartCoroutine(agent.WeaponOwner.ActivateWeapon());
        agent.WeaponOwner.SetTarget(agent.MainPlayer.transform);
        agent.NavMeshAgent.stoppingDistance = 1.5f;
    }

    public void Exit(AIAgent agent)
    {
        agent.NavMeshAgent.stoppingDistance = 0.0f;
    }

    public void Update(AIAgent agent)
    {
        agent.NavMeshAgent.destination = agent.MainPlayer.transform.position;
    }
}
