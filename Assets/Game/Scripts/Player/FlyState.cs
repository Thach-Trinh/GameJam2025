using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyState : PlayerBaseState
{
    private static int PREPARE_HASH = Animator.StringToHash("PrepareFly");
    private static int MOVE_HASH = Animator.StringToHash("MoveFly");
    public float epsilon = 0.001f;
    public float moveSpeed = 1f;
    public float prepareAnimSpeed = 1f;
    public float defaultMoveAnimSpeed = 0.1f;
    public float normalizedTransitionDuration = 0.1f;
    private Vector2 endPoint;
    private Transform trans;
    private bool isMoving;
    private bool isPrepareFly;
    private float prepareTicker = 0;
    private float prepareDuration = 0.8f;
    public override void Init(Player player)
    {
        base.Init(player);
        trans = player.transform;
    }
    public override void EnterState(ActionData data)
    {
        FlyActionData flyData = data as FlyActionData;
        endPoint = flyData.endPoint.position;
        isMoving = false;
        isPrepareFly = true;
        prepareTicker = 0;
        //player.SetStateAnimSpeed(prepareAnimSpeed);
        //player.anim.CrossFade(PREPARE_HASH, 0);
        //player.onFlyEventTrigger = StartMove;
    }

    private void StartMove()
    {
        isMoving = true;
        AudioController.Instance.PlaySound(SoundName.FLY);
        player.anim.CrossFade(MOVE_HASH, normalizedTransitionDuration);
        player.SetStateAnimSpeed(moveSpeed / defaultMoveAnimSpeed);
    }

    public override void UpdateState(float deltaTime, float timeScale)
    {
        if (isPrepareFly)
        {
            prepareTicker += deltaTime * timeScale;
            var delta = prepareTicker / prepareDuration;
            player.anim.Play(PREPARE_HASH, 0, delta);
            if (prepareTicker >= prepareDuration)
            {
                isPrepareFly = false;
                player.SetStateAnimSpeed(moveSpeed / defaultMoveAnimSpeed);
                StartMove();
            }
            return;
        }
        
        if (!isMoving)
            return;
        
        trans.position = Vector2.MoveTowards(trans.position, endPoint, deltaTime * timeScale * moveSpeed);
        if (Vector2.Distance(trans.position, endPoint) < epsilon)
            player.ChangeState(ActionType.Run);
    }

    public override void ExitState()
    {
        AudioController.Instance.StopSound(SoundName.FLY);
    }
}
