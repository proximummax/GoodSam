using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToPosition : ActionNode
{
    public float speed = 5;
    public float stoppingDistance = 0.1f;
    public bool updateRotation = true;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;

    protected override void OnStart() {
        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;
        context.agent.destination = blackboard.moveToPosition;
        context.agent.updateRotation = updateRotation;
        context.agent.acceleration = acceleration;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.agent.pathPending) {
           // context.animationComponent.SetRunState(true);
            return State.Running;
        }

        if (context.agent.remainingDistance < tolerance) {
          //  context.animationComponent.SetRunState(false);
            return State.Success;
        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid) {
          //  context.animationComponent.SetRunState(false);
            return State.Failure;
        }
       // context.animationComponent.SetRunState(true);
        return State.Running;
    }
}
