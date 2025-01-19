using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredObstacle : ObstacleBaseGeneric<ScreamingActionData>
{
    protected override void OnPlayerEnterEndTriggerBox()
    {
        base.OnPlayerEnterEndTriggerBox();
        _obstacleView.PlayObstacleExitTriggerBoxAnimation(_isCorrectChoice);
    }
    public override void OnPlayerSuccessInteract()
    {
        base.OnPlayerSuccessInteract();
        _obstacleView.OnPlayerSuccessInteract();
    }
}
