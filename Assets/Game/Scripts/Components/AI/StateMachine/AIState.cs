public enum AIStateID
{
    ChasePlayer, Death, Idle, FindWeapon, AttackPlayer, FindHealth, FindAmmo
}
public interface AIState
{
    AIStateID GetID();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}
