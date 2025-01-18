using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class DuckState : PlayerBaseState
{
    private static int PREPARE_HASH = Animator.StringToHash("PrepareDuck");
    private static int MOVE_HASH = Animator.StringToHash("MoveDuck");
    public float epsilon = 0.001f;
    public float moveSpeed = 1f;
    public float prepareAnimSpeed = 1f;
    public float defaultMoveAnimSpeed = 0.1f;

    public float normalizedTransitionDuration = 0.1f;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Transform trans;
    private bool isMoving;
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
        trans.position = Vector2.MoveTowards(trans.position, endPoint, deltaTime * timeScale * moveSpeed);
        if (Vector2.Distance(trans.position, endPoint) < epsilon)
            player.ChangeState(ActionType.Run);
    }

    public override void ExitState()
    {
    }
}
