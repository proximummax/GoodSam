using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFindWeaponState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.FindWeapon;
    }
    public void Enter(AIAgent agent)
    {
        WeaponPickup pickup = FindClosestWeapon(agent);
        agent.NavMeshAgent.destination = pickup.transform.position;
        agent.NavMeshAgent.speed = 5.0f;
    }

    public void Exit(AIAgent agent)
    {
        
    }

    public void Update(AIAgent agent)
    {
        Debug.Log("call upd");
        if (agent.NavMeshAgent.hasPath)
            agent.AnimationController.SetSpeed(agent.NavMeshAgent.velocity.magnitude);
        else
            agent.AnimationController.SetSpeed(0);

        if (agent.WeaponOwner.GetActiveWeapon())
        {
            Debug.Log("has weapon");
            agent.StateMachine.ChangeState(AIStateID.AttackPlayer);
        }
    }
    private WeaponPickup FindClosestWeapon(AIAgent agent)
    {
        WeaponPickup[] weapons = Object.FindObjectsOfType<WeaponPickup>();
        WeaponPickup closestWeapon = null;
        float closestDistance = float.MaxValue;
        foreach(var weapon in weapons)
        {
            float distanceToWeapon = Vector3.Distance(agent.transform.position, weapon.transform.position);
            if(distanceToWeapon < closestDistance)
            {
                closestDistance = distanceToWeapon;
                closestWeapon = weapon;
            }
        }
        return closestWeapon;
    }
}
