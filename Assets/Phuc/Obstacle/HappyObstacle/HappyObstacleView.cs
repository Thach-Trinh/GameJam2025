using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyObstacleView : ObstacleView
{
    [SerializeField] private Animator _animator;
    private static readonly int JumpHash = Animator.StringToHash(Jump);
    private const string Jump = "Jump";
    
    public override void PlayObstacleEnterTriggerBoxAnimation()
    {
        TriggerJump();
    }
    
    void TriggerJump()
    {
        if (_animator == null) return;
        _animator.SetTrigger(JumpHash);
    }
    
    public override void OnTimeScaleChanged(float timeScale)
    {
        if (_animator == null) return;
        _animator.speed = timeScale;
    }
}
