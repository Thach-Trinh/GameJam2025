using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;


public class Player : MonoBehaviour
{
    //public ActionType curAction;
    public ActionType nextAction;
    public AnimationEventHandler animEventHandler;
    private object[] nextData;
    [SerializeField] private PlayerBaseState[] states;
    public PlayerBaseState curState;
    private void Awake()
    {
        states.Iterate(x => x.Init(this));
        animEventHandler.onEventTrigger += OnAnimEventTrigger;
    }

    public void ReceiveAction(ActionType type, params object[] data)
    {
        nextAction = type;
        this.nextData = data;
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



    public void OnFinishObstacle()
    {
        nextAction = ActionType.Idle;
        nextData = null;
        ChangeState(ActionType.Run);
    }
}
