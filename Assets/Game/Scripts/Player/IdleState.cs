using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    private static int IDLE_HASH = Animator.StringToHash("Idle");
    public float normalizedTransitionDuration;
    //public int layer;
    public override void EnterState(ActionData data)
    {
        player.anim.CrossFade(IDLE_HASH, normalizedTransitionDuration);
    }

    public override void UpdateState(float deltaTime, float timeScale)
    {

    }

    public override void ExitState()
    {
        
    }
}
