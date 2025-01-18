using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepressedObstacle : ObstacleBaseGeneric<DuckActionData>
{
    protected override void OnPlayerEnterStartTriggerBox()
    {
        base.OnPlayerEnterStartTriggerBox();
        _obstacleView.PlayObstacleEnterTriggerBoxAnimation();
    }
    
    protected override void OnPlayerEnterEndTriggerBox()
    {
        base.OnPlayerEnterEndTriggerBox();
        _obstacleView.PlayObstacleExitTriggerBoxAnimation();
    }
}
