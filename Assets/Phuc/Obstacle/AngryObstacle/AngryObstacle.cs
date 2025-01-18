using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryObstacle : ObstacleBaseGeneric<AttackActionData>
{
    [SerializeField] private float durationDelayExitAnim;
    protected override void OnPlayerEnterStartTriggerBox()
    {
        base.OnPlayerEnterStartTriggerBox();
        _obstacleView.PlayObstacleEnterTriggerBoxAnimation();
    }
    
    protected override void OnPlayerEnterEndTriggerBox()
    {
        base.OnPlayerEnterEndTriggerBox();
        if (_isCorrectChoice)
        {
            _obstacleView.PlayObstacleExitAnimationWithDelayDuration(durationDelayExitAnim);
        }
    }
}
