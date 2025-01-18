using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    [SerializeField] private ObstacleTriggerBox _startTriggerBox;
    [SerializeField] private ObstacleTriggerBox _endTriggerBox;
    [SerializeField] protected ObstacleView _obstacleView;
    [SerializeField] private ObstacleData _obstacleData;
    public ObstacleData ObstacleData => _obstacleData;
    [SerializeField] protected bool _isCorrectChoice;
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

    protected virtual void OnPlayerEnterStartTriggerBox()
    {
        Debug.Log("Player entered start trigger box");
    }
    
    protected virtual void OnPlayerEnterEndTriggerBox()
    {
        Debug.Log("Player entered end trigger box");
    }

    private void OnDestroy()
    {
        _startTriggerBox._onPlayerTriggerObstacleBox -= OnPlayerEnterStartTriggerBox;
        _endTriggerBox._onPlayerTriggerObstacleBox -= OnPlayerEnterEndTriggerBox;
    }
}
