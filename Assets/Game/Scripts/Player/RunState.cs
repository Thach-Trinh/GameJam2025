using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    private static int SAD_HASH = Animator.StringToHash("SadWalk");
    private static int HAPPY_HASH = Animator.StringToHash("HappyWalk");
    public float sadSpeed = 2f;
    public float happySpeed = 2f;
    public float defaultSadAnimSpeed = 0.2f;
    public float defaultHappyAnimSpeed = 0.2f;
    public int happyThreshold = 3;
    protected Transform trans;
    public float normalizedTransitionDuration;
    private float speed;


    public override void Init(Player player)
    {
        base.Init(player);
        trans = player.transform;
    }

    public override void EnterState(ActionData data)
    {
        Animator anim = player.anim;
        bool isHappy = player.happyPoint >= happyThreshold;
        speed = isHappy ? happySpeed : sadSpeed;
        int hash = isHappy ? HAPPY_HASH : SAD_HASH;
        float defaultAnimSpeed = isHappy ? defaultHappyAnimSpeed : defaultSadAnimSpeed;
        anim.CrossFade(hash, normalizedTransitionDuration);
        player.SetStateAnimSpeed(speed / defaultAnimSpeed);
        
    }

    public override void UpdateState(float deltaTime, float timeScale)
    {
        trans.Translate(speed * deltaTime * timeScale * Vector2.right);
    }


    public override void ExitState()
    {
    }
}
