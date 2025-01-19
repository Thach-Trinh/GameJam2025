using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredObstacleView : ObstacleView
{
    public override void PlayObstacleExitTriggerBoxAnimation(bool isCorrectChoice)
    {
        if (!isCorrectChoice)
        {
            StartCoroutine(PlayScaredAnimationChoiceInCorrectIE());
        }
    }
    
    public override void OnPlayerSuccessInteract()
    {
        StartCoroutine(PlayScaredAnimationChoiceCorrectIE());
    }

    protected virtual IEnumerator PlayScaredAnimationChoiceCorrectIE()
    {
        yield return null;
    }

    protected virtual IEnumerator PlayScaredAnimationChoiceInCorrectIE()
    {
        yield return null;
    }
}
