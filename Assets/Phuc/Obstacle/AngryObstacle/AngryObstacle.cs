using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryObstacle : ObstacleBaseGeneric<AttackActionData>
{
    protected override void OnPlayerEnterStartTriggerBox()
    {
        base.OnPlayerEnterStartTriggerBox();
        _obstacleView.PlayObstacleEnterTriggerBoxAnimation();
    }
    
    public override void OnPlayerSuccessInteract()
    {
        base.OnPlayerSuccessInteract();
        if (_isCorrectChoice)
        {
            _obstacleView.OnPlayerSuccessInteract();
        }
    }
}
