using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class EnemyPosition : ActionNode
{
    private FirstPersonController _mainPlayer;
    protected override void OnStart()
    {
        _mainPlayer = FindObjectOfType<FirstPersonController>();
        if(_mainPlayer == null)
        {
            Debug.LogWarning("Main player not found");
        }
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        blackboard.moveToPosition = _mainPlayer.transform.position;
        return State.Success;
    }
}
