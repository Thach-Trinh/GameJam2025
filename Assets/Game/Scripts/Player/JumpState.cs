using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using Unity.VisualScripting;
using UnityEngine;

public enum JumpPhaseType
{
    Prepare,
    Up,
    Down,
    Land
}


public class JumpState : PlayerBaseState
{
    private const float GRAVITY = 9.81F;
    private static int PREPARE_HASH = Animator.StringToHash("PrepareJump");
    private static int UP_HASH = Animator.StringToHash("JumpUp");
    private static int DOWN_HASH = Animator.StringToHash("JumpDown");
    private static int LAND_HASH = Animator.StringToHash("LandJump");

    public float prepareSpeed = 1f;
    public float landSpeed = 1f;
    public float normalizedTransitionDuration = 0.1f;
    public Vector2 prepareOffset = new Vector2(-0.47f, 0.79f);
    public Vector2 startOffset = new Vector2(-0.47f, 0.79f);
    public Vector2 middleOffset;//-0.47,-0.99
    public Vector2 endOffset;//-0.52,0.71

    public JumpPhaseType curPhase;
    private Transform trans;
    public float upAnimDuration = 1.333f;
    public float downAnimDuration = 0.583f;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private float height;
    private float simulatedTotalDuration;
    private float simulatedUpDuration;
    private float simulatedDownDuration;
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
        simulatedUpDuration = requiredInitialVelocity / GRAVITY;
        simulatedDownDuration = Mathf.Sqrt(2 * GRAVITY * (startPoint.y + height - endPoint.y)) / GRAVITY;
        simulatedTotalDuration = simulatedUpDuration + simulatedDownDuration;
        curPhase = JumpPhaseType.Prepare;
        player.SetStateAnimSpeed(prepareSpeed);
        player.anim.CrossFade(PREPARE_HASH, normalizedTransitionDuration);
        player.onStartJumpEventTrigger = StartJump;
        trans.position = startPoint + prepareOffset;
        //float sum = requiredInitialVelocity + Mathf.Sqrt(2 * GRAVITY * (startPoint.y + height - endPoint.y));
        //simulatedDuration = sum / GRAVITY;        
    }

    private void StartJump()
    {
        curPhase = JumpPhaseType.Up;
        //trans.position = startPoint + prepareOffset;
        player.anim.CrossFade(UP_HASH, normalizedTransitionDuration);
        float requiredUpDuration = requiredDuration * simulatedUpDuration / simulatedTotalDuration;
        player.SetStateAnimSpeed(upAnimDuration / requiredUpDuration);
    }

    private void FinishJump()
    {
        player.ChangeState(ActionType.Run);
    }

    public override void UpdateState(float deltaTime, float timeScale)
    {
        switch (curPhase)
        {
            case JumpPhaseType.Up:
                {
                    float t = elapsedTime / requiredDuration;
                    float x = Mathf.Lerp(startPoint.x, endPoint.x, t);
                    float simulatedTime = t * simulatedTotalDuration;
                    float y = -GRAVITY * Mathf.Pow(simulatedTime, 2) / 2 + requiredInitialVelocity * simulatedTime + startPoint.y;
                    float lerp = Mathf.InverseLerp(0, simulatedUpDuration, simulatedTime);
                    Vector2 offset = Vector2.Lerp(startOffset, middleOffset, lerp);
                    //Vector2 offset = Vector2.zero;
                    trans.position = new Vector2(x, y) + offset;
                    if (simulatedTime >= simulatedUpDuration)
                    {
                        curPhase = JumpPhaseType.Down;
                        player.anim.CrossFade(DOWN_HASH, normalizedTransitionDuration);
                        float requiredDownDuration = requiredDuration * simulatedDownDuration / simulatedTotalDuration;
                        player.SetStateAnimSpeed(downAnimDuration / requiredDownDuration);
                    }
                    elapsedTime += deltaTime * timeScale;
                    break;
                }
            case JumpPhaseType.Down:
                {
                    float t = elapsedTime / requiredDuration;
                    float x = Mathf.Lerp(startPoint.x, endPoint.x, t);
                    float simulatedTime = t * simulatedTotalDuration;
                    float y = -GRAVITY * Mathf.Pow(simulatedTime, 2) / 2 + requiredInitialVelocity * simulatedTime + startPoint.y;
                    float lerp = Mathf.InverseLerp(simulatedUpDuration, simulatedTotalDuration, simulatedTime);
                    Vector2 offset = Vector2.Lerp(middleOffset, endOffset, lerp);
                    //Vector2 offset = Vector2.zero;
                    trans.position = new Vector2(x, y) + offset;
                    elapsedTime += deltaTime * timeScale;
                    if (elapsedTime >= requiredDuration)
                    {
                        curPhase = JumpPhaseType.Land;
                        player.anim.CrossFade(LAND_HASH, normalizedTransitionDuration);
                        player.SetStateAnimSpeed(landSpeed);
                        player.onFinishJumpEventTrigger = FinishJump;
                    }
                    break;
                }
        }
    }


    //public override void UpdateState(float deltaTime, float timeScale)
    //{
    //    if (curPhase == JumpPhaseType.Prepare || curPhase == JumpPhaseType.Land)
    //        return;
    //    float t = elapsedTime / requiredDuration;
    //    float x = Mathf.Lerp(startPoint.x, endPoint.x, t);
    //    float simulatedTime = t * simulatedTotalDuration;
    //    float y = -GRAVITY * Mathf.Pow(simulatedTime, 2) / 2 + requiredInitialVelocity * simulatedTime + startPoint.y;
    //    trans.position = new Vector2(x, y);
    //    elapsedTime += deltaTime * timeScale;
    //    if (elapsedTime >= requiredDuration)
    //    {
    //        player.ChangeState(ActionType.Run);
    //    }
    //}


    public override void ExitState()
    {
        trans.position = endPoint;
    }

    //public static Vector2 Remap(Vector2 value, Vector2 inMin, Vector2 inMax, Vector2 outMin, Vector2 outMax)
    //{
    //    float tX = Mathf.InverseLerp(inMin.x, inMax.x, value.x);
    //    float tY = Mathf.InverseLerp(inMin.y, inMax.y, value.y);
    //    float newX = Mathf.Lerp(outMin.x, outMax.x, tX);
    //    float newY = Mathf.Lerp(outMin.y, outMax.y, tY);

    //    return new Vector2(newX, newY);
    //}
}
