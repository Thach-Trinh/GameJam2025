using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryObstacleView : ObstacleView
{
    [SerializeField] private Animator _animator;
    private static readonly int TriggeredHash = Animator.StringToHash(Triggered);
    private const string Triggered = "Triggered";
    
    private static readonly int DisappearHash = Animator.StringToHash(Disappear);
    private const string Disappear = "Disappear";
    
    public override void PlayObstacleEnterTriggerBoxAnimation()
    {
        if (_animator != null)
        {
            TriggerAnimation(TriggeredHash);
        }
    }
    
    public override void PlayObstacleExitAnimationWithDelayDuration(float duration)
    {
        if (_animator != null)
        {
            StartCoroutine(DelayExitAnimation(duration));
        }
    }
    
    IEnumerator DelayExitAnimation(float duration)
    {
        float ticker = 0;
        while (ticker < duration)
        {
            ticker += Time.deltaTime*_globalTimeScale;
            yield return null;
        }
        TriggerAnimation(DisappearHash);
    }
    
    void TriggerAnimation(int hash)
    {
        _animator.SetTrigger(hash);
    }
}
