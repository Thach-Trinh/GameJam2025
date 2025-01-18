using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamState : PlayerBaseState
{
    public float animSpeed = 1f;
    public float normalizedTransitionDuration = 0.1f;
    private static int HASH = Animator.StringToHash("Scream");
    public override void EnterState(ActionData data)
    {
        player.SetStateAnimSpeed(animSpeed);
        player.anim.CrossFade(HASH, normalizedTransitionDuration);
        player.onStopScreamEventTrigger = Stop;
    }

    public void Stop()
    {
        player.ChangeState(ActionType.Run);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState(float deltaTime, float timeScale)
    {
        
    }
}
