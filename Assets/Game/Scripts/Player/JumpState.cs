using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    private const float GRAVITY = 9.81F;
    protected Transform trans;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private float height;
    private float simulatedDuration;
    private float requiredDuration;
    private float elapsedTime;
    private float requiredInitialVelocity;
    public override void Init(Player player)
    {
        base.Init(player);
        trans = player.transform;
    }

    public override void EnterState(ActionData data)
    {
        JumpActionData jumpData = data as JumpActionData;
        startPoint = trans.position;
        endPoint = jumpData.endPoint.position;
        height = jumpData.height;
        requiredDuration = jumpData.duration;
        elapsedTime = 0;
        requiredInitialVelocity = Mathf.Sqrt(2 * height * GRAVITY);
        //float flyUp = requiredInitialVelocityY / GRAVITY;
        //float flyDown = Mathf.Sqrt(2 * GRAVITY * (startPoint.y + height - endPoint.y)) / GRAVITY;
        //simulatedDuration = flyUp + flyDown;
        float sum = requiredInitialVelocity + Mathf.Sqrt(2 * GRAVITY * (startPoint.y + height - endPoint.y));
        simulatedDuration = sum / GRAVITY;        
    }



    public override void UpdateState(float deltaTime, float timeScale)
    {
        float t = elapsedTime / requiredDuration;
        float x = Mathf.Lerp(startPoint.x, endPoint.x, t);
        float simulatedTime = t * simulatedDuration;
        float y = -GRAVITY * Mathf.Pow(simulatedTime, 2) / 2 + requiredInitialVelocity * simulatedTime + startPoint.y;
        trans.position = new Vector2(x, y);
        elapsedTime += deltaTime * timeScale;
        //Debug.Log($"Current ({x}, {y}) - start {startPoint} target {endPoint} - time {elapsedTime} - t {t}");
        //Debug.Log($"Current {y} - start {startPoint.y} target {endPoint.y} - time {elapsedTime} - t {t}");
        if (elapsedTime >= requiredDuration)
        {
            Debug.Log("Done");
            player.ChangeState(ActionType.Run);
        }
    }


    public override void ExitState()
    {
    }
}
