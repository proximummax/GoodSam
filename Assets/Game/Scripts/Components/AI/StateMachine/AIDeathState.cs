using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.Death;
    }

    public void Enter(AIAgent agent)
    {
        agent.Ragdoll.ActivateRagdoll();
        agent.HealthBar.gameObject.SetActive(false);
        agent.WeaponOwner.DropWeapon();
    }

    public void Exit(AIAgent agent)
    {
        
    }


    public void Update(AIAgent agent)
    {
       
    }


}
