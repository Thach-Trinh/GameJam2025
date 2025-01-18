using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleObstacle : MonoBehaviour
{
    [SerializeField] private float _timeScale = 1;
    [SerializeField] private ObstacleTriggerBox _obstacleTriggerBoxEnter;

    void Start()
    {
        ResetObstacle();
    } 
    
    public void ResetObstacle()
    {
        InitTimeScaleObstacle();
    } 
    void InitTimeScaleObstacle()
    {
        _obstacleTriggerBoxEnter.InitObstacleBox();
        _obstacleTriggerBoxEnter._onPlayerTriggerObstacleBox = null;
        _obstacleTriggerBoxEnter._onPlayerTriggerObstacleBox += OnPlayerTriggerEnterObstacleBox;
    }

    private void OnPlayerTriggerEnterObstacleBox()
    {
        TimeController.Instance.SetTimeScale(_timeScale);
    }
}
