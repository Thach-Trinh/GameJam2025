using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    protected float _globalTimeScale => TimeController.Instance != null? TimeController.Instance.curTimeScale : 1;
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

    public virtual void OnPlayerSuccessInteract()
    {
        // Play success interact animation
    }
}
