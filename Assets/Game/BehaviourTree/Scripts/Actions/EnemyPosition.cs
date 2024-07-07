using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class EnemyPosition : ActionNode
{
    private ThirdPlayerController _mainPlayer;
    protected override void OnStart()
    {
       
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
