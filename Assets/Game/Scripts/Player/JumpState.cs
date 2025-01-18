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
    private float requiredInitialVelocityY;
    public override void Init(Player player)
    {
        base.Init(player);
        trans = player.transform;
    }

    public override void EnterState(object[] data)
    {
        startPoint = trans.position;
        endPoint = (Vector2)data[0];
        height = (float)data[1];
        requiredDuration = (float)data[2];
        elapsedTime = 0;
        requiredInitialVelocityY = Mathf.Sqrt(2 * height * GRAVITY);
        float flyUp = requiredInitialVelocityY / GRAVITY;
        float flyDown = Mathf.Sqrt(2 * GRAVITY * (startPoint.y + height - endPoint.y)) / GRAVITY;
        simulatedDuration = flyUp + flyDown;
        //float sum = requiredInitialVelocityY + Mathf.Sqrt(2 * GRAVITY * (startPoint.y + height - endPoint.x));
        //simulatedDuration = sum / GRAVITY;
        //requiredInitialVelocityX = (endPoint.x - startPoint.x) / simulatedDuration;
        Debug.Log($"up {flyUp} - down {flyDown} - total {simulatedDuration}");

        float sum = requiredInitialVelocityY + Mathf.Sqrt(2 * GRAVITY * (startPoint.y + height - endPoint.x));
        Debug.Log(sum / GRAVITY);
    }



    public override void UpdateState(float deltaTime, float timeScale)
    {
        float t = elapsedTime / requiredDuration;
        float x = Mathf.Lerp(startPoint.x, endPoint.x, t);
        float simulatedTime = t * simulatedDuration;
        float y = -GRAVITY * Mathf.Pow(simulatedTime, 2) / 2 + requiredInitialVelocityY * simulatedTime + startPoint.y;
        trans.position = new Vector2(x, y);
        elapsedTime += deltaTime * timeScale;
        //Debug.Log($"Current ({x}, {y}) - start {startPoint} target {endPoint} - time {elapsedTime} - t {t}");
        Debug.Log($"Current {y} - start {startPoint.y} target {endPoint.y} - time {elapsedTime} - t {t}");
        if (elapsedTime >= requiredDuration)
        {
            Debug.Log("Done");
            player.ChangeState(ActionType.Idle);
        }
    }

    //public override void UpdateState(float deltaTime, float timeScale)
    //{
    //    float t = elapsedTime / simulatedDuration;
    //    //float x = Mathf.Lerp(startPoint.x, endPoint.x, t);
    //    float x = requiredInitialVelocityX * elapsedTime + startPoint.x;
    //    float simulatedTime = elapsedTime;
    //    float y = -GRAVITY * Mathf.Pow(simulatedTime, 2) / 2 + requiredInitialVelocityY * simulatedTime + startPoint.y;
    //    //trans.position = new Vector2(trans.position.x, y);
    //    trans.position = new Vector2(x, y);
    //    elapsedTime += deltaTime;
    //    //Debug.Log($"Current ({x}, {y}) - start {startPoint} target {endPoint} - time {elapsedTime} - t {t}");
    //    Debug.Log($"Current {y} - start {startPoint.y} target {endPoint.y} - height {height} - time {elapsedTime} - t {t}");
    //    if (elapsedTime >= simulatedDuration)
    //    {
    //        Debug.Log("Done");
    //        player.ChangeState(ActionType.Idle);
    //    }
    //}




    public override void ExitState()
    {
    }
}
