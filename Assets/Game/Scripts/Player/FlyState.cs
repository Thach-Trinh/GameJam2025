using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float centerOffset = 1f;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 center;
    private Transform trans;
    private bool isMoving;
    private float distance;
    private float duration;
    private float elapsedTime;
    public override void Init(Player player)
    {
        base.Init(player);
        trans = player.transform;
    }
    public override void EnterState(ActionData data)
    {
        FlyActionData flyData = data as FlyActionData;
        startPoint = trans.position;
        endPoint = flyData.endPoint.position;
        center = (startPoint + endPoint) / 2f - centerOffset * Vector2.up;
        //center.y = startPoint.y;
        isMoving = false;
        player.SetStateAnimSpeed(prepareAnimSpeed);
        player.anim.CrossFade(PREPARE_HASH, normalizedTransitionDuration);
        player.onFlyEventTrigger = StartMove;
        elapsedTime = 0;
        float distance = Vector2.Distance(startPoint, endPoint);
        duration = distance / moveSpeed;
    }

    private void StartMove()
    {
        isMoving = true;
        player.anim.CrossFade(MOVE_HASH, normalizedTransitionDuration);
        player.SetStateAnimSpeed(moveSpeed / defaultMoveAnimSpeed);
    }

    public override void UpdateState(float deltaTime, float timeScale)
    {
        if (!isMoving)
            return;
        //trans.position = Vector2.MoveTowards(trans.position, endPoint, deltaTime * timeScale * moveSpeed);
        elapsedTime += deltaTime * timeScale;
        float t = elapsedTime / duration;
        Vector3 slerp = Vector3.Slerp((Vector3)(startPoint - center), (Vector3)(endPoint - center), t);
        trans.position = new Vector2(slerp.x, slerp.y) + center;
        if (elapsedTime >= duration)
        //if (Vector2.Distance(trans.position, endPoint) < epsilon)
            player.ChangeState(ActionType.Run);
    }

    public override void ExitState()
    {
        trans.position = endPoint;
    }

}
