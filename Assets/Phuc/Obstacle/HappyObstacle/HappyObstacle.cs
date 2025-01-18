using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyObstacle : ObstacleBaseGeneric<JumpActionData>
{
    protected override void OnPlayerEnterStartTriggerBox()
    {
        base.OnPlayerEnterStartTriggerBox();
        _obstacleView.PlayObstacleEnterTriggerBoxAnimation();
    }
}
