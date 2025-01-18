using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;


public class Player : MonoBehaviour, ITimeReactive
{
    public static Player Instance;
    public ActionType nextAction;
    public AnimationEventHandler animEventHandler;
    public Animator anim;
    private ActionData nextData;
    [SerializeField] private PlayerBaseState[] states;
    public PlayerBaseState curState;
    private bool isCorrect;
    private float stateAnimSpeed;
    private void Awake()
    {
        Instance = this;
        states.Iterate(x => x.Init(this));
        animEventHandler.onEventTrigger += OnAnimEventTrigger;
    }

    private void OnEnable()
    {
        TimeController.Instance.affectedObjects.Add(this);
    }

    private void OnDisable()
    {
        TimeController.Instance.affectedObjects.Remove(this);
    }

    public void ReceiveAction(ActionType type, ActionData data, bool isCorrect)
    {
        nextAction = type;
        this.nextData = data;
        this.isCorrect = isCorrect;
    }

    public void ChangeState(ActionType type, ActionData data = null)
    {
        PlayerBaseState newState = Array.Find(states, (state) => state.Type == type);
        ChangeState(newState, data);
    }

    public void ChangeState(PlayerBaseState newState, ActionData data)
    {
        curState?.ExitState();
        curState = newState;
        curState?.EnterState(data);
    }

    public void CustomUpdate(float timeScale)
    {
        curState?.UpdateState(Time.deltaTime, timeScale);
    }

    public void SetStateAnimSpeed(float stateAnimSpeed)
    {
        this.stateAnimSpeed = stateAnimSpeed;
        anim.speed = stateAnimSpeed * TimeController.Instance.curTimeScale;
        Debug.Log(anim.speed);
    }

    public void OnTimeScaleChanged(float timeScale)
    {
        anim.speed = stateAnimSpeed * timeScale;
        Debug.Log(anim.speed);
    }

    public void OnAnimEventTrigger(int value)
    {
        switch (value)
        {
            case 0:
                {
                    Debug.Log("Attack");
                    CameraController.Instance.Shake();
                    ChangeState(ActionType.Run);
                    break;
                }
        }
    }

    public void InteractWithObstacle()
    {
        if (nextAction == ActionType.Idle || !isCorrect)
        {
            ChangeState(ActionType.Idle);
            return;
        }
        ChangeState(nextAction, nextData);
        OnFinishObstacle();
    }



    public void OnFinishObstacle()
    {
        nextAction = ActionType.Idle;
        nextData = null;
    }


}
