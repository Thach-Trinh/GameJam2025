using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : DepressedObstacleView
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _totalDuration;
    
    private static readonly int MoveHash = Animator.StringToHash(Move);
    private const string Move = "Move";
    protected override IEnumerator DepressedObstacleEnterTriggerBoxAnimation()
    {
        float ticker = 0;
        
        while (true)
        {
            ticker += Time.deltaTime;
            float delta = ticker / _totalDuration;
            _animator.Play(MoveHash, 0, delta);
            if (delta >= 1)
            {
                ticker = 0;
            }
            yield return null;
        }
    }
}
