public class AIDeathState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.Death;
    }

    public void Enter(AIAgent agent)
    {
        agent.Ragdoll.ActivateRagdoll();
        agent.WeaponOwner.DropWeapon();
    }

    public void Exit(AIAgent agent)
    {
        
    }


    public void Update(AIAgent agent)
    {
       
    }


}
