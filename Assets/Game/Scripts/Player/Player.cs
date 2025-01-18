using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;


public class Player : MonoBehaviour
{
    public static Player Instance;
    public ActionType nextAction;
    public AnimationEventHandler animEventHandler;
    public Animator anim;
    private ActionData nextData;
    [SerializeField] private PlayerBaseState[] states;
    public PlayerBaseState curState;
    private bool isCorrect;
    private void Awake()
    {
        Instance = this;
        states.Iterate(x => x.Init(this));
        animEventHandler.onEventTrigger += OnAnimEventTrigger;
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
