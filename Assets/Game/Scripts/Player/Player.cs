using System;
using System.Collections;
using System.Collections.Generic;
using BubblePopupNS;
using UnityEngine;


public class Player : MonoBehaviour, ITimeReactive
{
    public static Player Instance;
    
    public PlayerAnimationEventHandler animEventHandler;
    public Animator anim;
    public Action onStartJumpEventTrigger;
    public Action onFinishJumpEventTrigger;
    public Action onStartAttackEventTrigger;
    public Action onDealDamageEventTrigger;
    public Action onFinishAttackEventTrigger;
    public Action onDuckEventTrigger;
    public Action onFlyEventTrigger;
    public Action onStopScreamEventTrigger;
    public ActionType nextAction;
    private ActionData nextData;
    public ObstacleBase nextObstacle;
    public PlayerBaseState[] states;
    public PlayerBaseState curState;
    public int happyPoint;
    private bool isCorrect;
    private float stateAnimSpeed;
    private void Awake()
    {
        Instance = this;
        states.Iterate(x => x.Init(this));
        animEventHandler.onEventTrigger += OnAnimEventTrigger;
    }

    private void Start()
    {
        //Debug.Log(TimeController.Instance != null);
        //Debug.Log(TimeController.Instance.affectedObjects != null);
        TimeController.Instance.affectedObjects.Add(this);
    }


    private void OnDestroy()
    {
        TimeController.Instance.affectedObjects.Remove(this);
    }

    public void ReceiveEmotion(EmotionType type, ActionData data, bool isCorrect, ObstacleBase nextObstacle)
    {
        nextAction = BubbleBridge.GetAction(type);
        if ((type == EmotionType.Love || type == EmotionType.Happy) && isCorrect)
            happyPoint++;
        this.nextData = data;
        this.isCorrect = isCorrect;
        this.nextObstacle = nextObstacle;
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
        //Debug.Log(anim.speed);
    }

    public void OnTimeScaleChanged(float timeScale)
    {
        anim.speed = stateAnimSpeed * timeScale;
        //Debug.Log(anim.speed);
    }

    public void OnAnimEventTrigger(PlayerAnimationEventType type)
    {
        switch (type)
        {
            case PlayerAnimationEventType.FootStep:
                {
                    AudioController.Instance.PlaySound(SoundName.FOOTSTEP);
                    break;
                }
            case PlayerAnimationEventType.StartJump:
                {
                    onStartJumpEventTrigger?.Invoke();
                    onStartJumpEventTrigger = null;
                    break;
                }
            case PlayerAnimationEventType.FinishJump:
                {
                    onFinishJumpEventTrigger?.Invoke();
                    onFinishJumpEventTrigger = null;
                    break;
                }
            case PlayerAnimationEventType.StartAttack:
                {
                    onStartAttackEventTrigger?.Invoke();
                    onStartAttackEventTrigger = null;
                    break;
                }
            case PlayerAnimationEventType.DealDamage:
                {
                    CameraController.Instance.Shake();
                    nextObstacle?.OnPlayerSuccessInteract();
                    break;
                }
            case PlayerAnimationEventType.FinishAttack:
                {
                    onFinishAttackEventTrigger?.Invoke();
                    onFinishAttackEventTrigger = null;
                    break;
                }
            case PlayerAnimationEventType.Duck:
                {
                    onDuckEventTrigger?.Invoke();
                    onDuckEventTrigger = null;
                    break;
                }
            case PlayerAnimationEventType.Fly:
                {
                    onFlyEventTrigger?.Invoke();
                    onFlyEventTrigger = null;
                    break;
                }
            case PlayerAnimationEventType.StartScream:
                {
                    CameraController.Instance.Shake();
                    nextObstacle?.OnPlayerSuccessInteract();
                    break;
                }
            case PlayerAnimationEventType.StopScream:
                {
                    onStopScreamEventTrigger?.Invoke();
                    onStopScreamEventTrigger = null;
                    break;
                }
        }
    }

    public void InteractWithObstacle()
    {
        if (nextAction == ActionType.Idle || !isCorrect)
        {
            ChangeState(ActionType.Idle);
            if (GameController.Instance.CurrentState == GameController.Instance.GameState)
            {
                var playState = GameController.Instance.GameState as PlayState;
                playState?.OnPlayerDie();
            }
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
