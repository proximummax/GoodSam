using UnityEngine;
using UnityEngine.AI;

public class AIChasePlayerState : AIState
{



    private float _timer = 0.0f;

    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;
    }
    public void Enter(AIAgent agent)
    {

    }

    public void Exit(AIAgent agent)
    {

    }



    public void Update(AIAgent agent)
    {
        if (!agent.enabled)
            return;

        _timer -= Time.deltaTime;

        if (!agent.NavMeshAgent.hasPath)
        {
            agent.NavMeshAgent.destination = agent.MainPlayer.transform.position;
        }
        if (_timer < 0.0f)
        {
            Vector3 direction = (agent.MainPlayer.transform.position - agent.NavMeshAgent.destination);
            direction.y = 0.0f;
            if (direction.sqrMagnitude > Mathf.Pow(agent.Config.MaxDistance, 2))
            {
                if (agent.NavMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.NavMeshAgent.destination = agent.MainPlayer.transform.position;
                }
            }
            _timer = agent.Config.MaxTime;
        }

      //  if (agent.NavMeshAgent.hasPath)
        //    agent.AnimationController.SetSpeed(agent.NavMeshAgent.velocity.magnitude);
      //  else
       //     agent.AnimationController.SetSpeed(0);

    }

}
