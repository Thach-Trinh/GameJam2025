using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryObstacle : ObstacleBaseGeneric<AttackActionData>
{
    internal enum AngryObstacleType
    {
        BadRumor,
        Boss
    }
    [SerializeField] private AngryObstacleType _angryObstacleType;
    protected override void OnPlayerEnterStartTriggerBox()
    {
        base.OnPlayerEnterStartTriggerBox();
        _obstacleView.PlayObstacleEnterTriggerBoxAnimation();
        if (_angryObstacleType == AngryObstacleType.BadRumor)
        {
            AudioController.Instance.PlaySound(SoundName.WHISPERS);
        }
        else if (_angryObstacleType == AngryObstacleType.Boss)
        {
            AudioController.Instance.PlaySound(SoundName.BOSS_YELLING);
        }
    }
    
    protected override void OnPlayerEnterEndTriggerBox()
    {
        base.OnPlayerEnterEndTriggerBox();
        if (_angryObstacleType == AngryObstacleType.BadRumor)
        {
            AudioController.Instance.StopSound(SoundName.WHISPERS);
        }
        else if (_angryObstacleType == AngryObstacleType.Boss)
        {
            AudioController.Instance.StopSound(SoundName.BOSS_YELLING);
        }
    }
    
    public override void OnPlayerSuccessInteract()
    {
        base.OnPlayerSuccessInteract();
        if (_isCorrectChoice)
        {
            _obstacleView.OnPlayerSuccessInteract();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_angryObstacleType == AngryObstacleType.BadRumor)
        {
            if (AudioController.Instance == null) return;
            AudioController.Instance.StopSound(SoundName.WHISPERS);
        }
        else if (_angryObstacleType == AngryObstacleType.Boss)
        {
            if (AudioController.Instance == null) return;
            AudioController.Instance.StopSound(SoundName.BOSS_YELLING);
        }
    }
}
