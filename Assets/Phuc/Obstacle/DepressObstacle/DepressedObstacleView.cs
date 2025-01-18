using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepressedObstacleView : ObstacleView
{
    public override void PlayObstacleEnterTriggerBoxAnimation()
    {
        StartCoroutine(DepressedObstacleEnterTriggerBoxAnimation());
    }
    
    protected virtual IEnumerator DepressedObstacleEnterTriggerBoxAnimation()
    {
        // Play enter animation
        yield return null;
    }
    
    public override void PlayObstacleExitTriggerBoxAnimation()
    {
        StartCoroutine(DepressedObstacleExitTriggerBoxAnimation());
    }
    
    protected virtual IEnumerator DepressedObstacleExitTriggerBoxAnimation()
    {
        // Play enter animation
        yield return null;
    }
}
