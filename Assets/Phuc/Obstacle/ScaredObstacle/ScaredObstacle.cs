using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredObstacle : ObstacleBaseGeneric<ScreamingActionData>
{
    protected override void OnPlayerEnterEndTriggerBox()
    {
        base.OnPlayerEnterStartTriggerBox();
        _obstacleView.PlayObstacleExitTriggerBoxAnimation(_isCorrectChoice);
    }
}
