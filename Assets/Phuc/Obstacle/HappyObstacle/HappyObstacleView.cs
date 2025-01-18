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
        if (_animator != null)
        {
            InvokeRepeating(nameof(TriggerJump),0f,2f);
        }
    }
    
    void TriggerJump()
    {
        _animator.SetTrigger(JumpHash);
    }
}
