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
    public abstract ActionData GetData();
    void Start()
    {
        _isCorrectChoice = false;
        InitTriggerBoxes();
        RegisterEvents();
    }
    
    void InitTriggerBoxes()
    {
        _startTriggerBox.InitObstacleBox(this);
        _endTriggerBox.InitObstacleBox(this);
    }
    
    void RegisterEvents()
    {
        _startTriggerBox._onPlayerTriggerObstacleBox += OnPlayerEnterStartTriggerBox;
        _endTriggerBox._onPlayerTriggerObstacleBox += OnPlayerEnterEndTriggerBox;
    }
    
    public void ChoiceCorrect()
    {
        _isCorrectChoice = true;
    }

    protected virtual void OnPlayerEnterStartTriggerBox()
    {
        Debug.Log("Player entered start trigger box");
        BubblePopupController.Instance.ShowPopup(_obstacleData,this);
    }
    
    protected virtual void OnPlayerEnterEndTriggerBox()
    {
        Debug.Log("Player entered end trigger box");
        Player.Instance.InteractWithObstacle();
    }

    private void OnDestroy()
    {
        _startTriggerBox._onPlayerTriggerObstacleBox -= OnPlayerEnterStartTriggerBox;
        _endTriggerBox._onPlayerTriggerObstacleBox -= OnPlayerEnterEndTriggerBox;
    }
}
