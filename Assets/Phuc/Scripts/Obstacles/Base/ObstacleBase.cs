using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VyNS;

public abstract class ObstacleBase : MonoBehaviour
{
    [SerializeField] private ObstacleTriggerBox _startTriggerBox;
    [SerializeField] private ObstacleTriggerBox _endTriggerBox;
    [SerializeField] protected ObstacleView _obstacleView;
    [SerializeField] private BubblePopupData _obstacleData;
    [SerializeField] protected bool _isCorrectChoice;
    [SerializeField] private float customTimeScale;
    public abstract ActionData GetData();
    void Start()
    {
        ResetObstacle();
    }

    public void ResetObstacle()
    {
        _isCorrectChoice = false;
        InitTriggerBoxes();
        RegisterEvents();
    }

    void InitTriggerBoxes()
    {
        _startTriggerBox.InitObstacleBox();
        _endTriggerBox.InitObstacleBox();
    }
    
    void RegisterEvents()
    {
        _startTriggerBox._onPlayerTriggerObstacleBox = null;
        _startTriggerBox._onPlayerTriggerObstacleBox += OnPlayerEnterStartTriggerBox;
        _endTriggerBox._onPlayerTriggerObstacleBox = null;
        _endTriggerBox._onPlayerTriggerObstacleBox += OnPlayerEnterEndTriggerBox;
    }
    
    public void ChoiceCorrect()
    {
        _isCorrectChoice = true;
    }

    protected virtual void OnPlayerEnterStartTriggerBox()
    {
        Debug.Log("Player entered start trigger box");
        if (BubblePopupController.Instance != null)
        {
            TimeController.Instance.SetTimeScale(customTimeScale);
            var popupData = ScriptableObject.CreateInstance<BubblePopupData>();
            popupData.emotionVisuals = new List<EmotionBubbleVisualData>(_obstacleData.EmotionBubbleVisualDatas);
            popupData.hint = _obstacleData.Hint;
            float time = GetPopupTime();
            popupData.selectionTime = time > 0 ? time : _obstacleData.SelectionTime;
            popupData.emotionVisuals.Shuffle();
            BubblePopupController.Instance.ShowPopup(popupData,this);
        }
    }
    
    float GetPopupTime()
    {
        float distance = _endTriggerBox.transform.position.x - _startTriggerBox.transform.position.x;
        var playerSpeed = Player.Instance.curState as RunState;
        var timeScale = TimeController.Instance.curTimeScale;
        if (timeScale != 0 && playerSpeed != null)
        {
            var returnTime = distance / (playerSpeed.speed * timeScale);
            return returnTime;
        }

        return -1;
    }
    
    protected virtual void OnPlayerEnterEndTriggerBox()
    {
        Debug.Log("Player entered end trigger box");
        if (Player.Instance != null)
        {
            Player.Instance.InteractWithObstacle();
        }
    }
    
    public virtual void OnPlayerSuccessInteract()
    {
        Debug.Log("Player success interact with obstacle");
    }

    protected virtual void OnDestroy()
    {
        _startTriggerBox._onPlayerTriggerObstacleBox -= OnPlayerEnterStartTriggerBox;
        _endTriggerBox._onPlayerTriggerObstacleBox -= OnPlayerEnterEndTriggerBox;
    }
}
