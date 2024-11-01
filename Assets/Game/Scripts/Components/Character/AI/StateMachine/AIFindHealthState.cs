using UnityEngine;

public class AIFindHealthState : AIState
{
    private GameObject _pickup;
    private GameObject[] _pickups = new GameObject[1];
    public AIStateID GetID()
    {
        return AIStateID.FindHealth;
    }
    public void Enter(AIAgent agent)
    {
        _pickup = null;
        agent.NavMeshAgent.speed = 6.0f;
    }

    public void Exit(AIAgent agent)
    {

    }

    public void Update(AIAgent agent)
    {
        if (!_pickup)
        {
            _pickup = FindPickup(agent);
            if (_pickup)
            {
                CollectPickup(agent, _pickup);
            }
        }

        if (!agent.NavMeshAgent.hasPath)
        {
            Vector3 randomWay = new Vector3(
                Random.Range(agent.LevelBounds.Min.position.x, agent.LevelBounds.Max.position.x),
                Random.Range(agent.LevelBounds.Min.position.y, agent.LevelBounds.Max.position.y),
                Random.Range(agent.LevelBounds.Min.position.z, agent.LevelBounds.Max.position.z));
            agent.NavMeshAgent.destination = randomWay;
        }

        if (agent.NavMeshAgent.hasPath)
            agent.AnimationController.SetSpeed(agent.NavMeshAgent.velocity.magnitude);
        else
            agent.AnimationController.SetSpeed(0);


        if (!agent.BaseHealthComponent.IsLowHealth())
        {
            agent.StateMachine.ChangeState(AIStateID.AttackPlayer);
        }
    }
    private GameObject FindPickup(AIAgent agent)
    {
        int count = agent.AISensor.Filter(_pickups, "Pickup", "Health");
        return count > 0 ? _pickups[0] : null;
    }
    private void CollectPickup(AIAgent agent, GameObject pickup)
    {
        agent.NavMeshAgent.destination = pickup.transform.position;
    }
}
