using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckState : PlayerBaseState
{
    private static int PREPARE_HASH = Animator.StringToHash("PrepareDuck");
    private static int MOVE_HASH = Animator.StringToHash("MoveDuck");
    public float epsilon = 0.001f;
    public float moveSpeed = 1f;
    public float prepareAnimSpeed = 1f;
    public float defaultMoveAnimSpeed = 0.1f;
    public Vector2 offset = new Vector2(0, -1.25f);
    public float normalizedTransitionDuration = 0.1f;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Transform trans;
    private bool isMoving;
    private Vector2 curPos;
    public override void Init(Player player)
    {
        base.Init(player);
        trans = player.transform;
    }
    public override void EnterState(ActionData data)
    {
        DuckActionData duckData = data as DuckActionData;
        startPoint = trans.position;
        endPoint = duckData.pointToEndDuck.position;
        isMoving = false;
        player.SetStateAnimSpeed(prepareAnimSpeed);
        player.anim.CrossFade(PREPARE_HASH, normalizedTransitionDuration);
        player.onDuckEventTrigger = StartMove;
        curPos = startPoint;
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
        curPos = Vector2.MoveTowards(curPos, endPoint, deltaTime * timeScale * moveSpeed);
        trans.position = curPos + offset;
        if (Vector2.Distance(curPos, endPoint) < epsilon)
            player.ChangeState(ActionType.Run);
    }

    public override void ExitState()
    {
        trans.position = endPoint;
    }
}
