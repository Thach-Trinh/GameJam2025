using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveObstacle : ObstacleBaseGeneric<FlyActionData>
{
    protected override void OnPlayerEnterStartTriggerBox()
    {
        base.OnPlayerEnterStartTriggerBox();
        _obstacleView.PlayObstacleEnterTriggerBoxAnimation();
    }
}
