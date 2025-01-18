using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveObstacleView : ObstacleView
{
    [SerializeField] private Animator _animator;
    private static readonly int MoveHash = Animator.StringToHash(Move);
    private const string Move = "Move";
    
    public override void PlayObstacleEnterTriggerBoxAnimation()
    {
        if (_animator != null)
        {
            _animator.SetTrigger(MoveHash);
        }
    }
}
