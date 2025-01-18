using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    protected float _globalTimeScale => TimeController.Instance.curTimeScale;
    public virtual void PlayObstacleEnterTriggerBoxAnimation()
    {
        // Play enter animation
    }
    
    public virtual void PlayObstacleExitTriggerBoxAnimation()
    {
        // Play exit animation
    }
    
    public virtual void PlayObstacleExitTriggerBoxAnimation(bool isCorrectChoice)
    {
        // Play exit animation
    }
    
    public virtual void PlayObstacleExitAnimationWithDelayDuration(float duration)
    {
        // Play exit animation with delay duration
    }
}
