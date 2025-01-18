using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public static ObstacleController Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private List<ObstacleBase> _obstacleList;
    [SerializeField] private List<TimeScaleObstacle> _timeScaleObstacleList;
    [SerializeField] private WinTriggerBox _winTriggerBox;
    
    [ContextMenu("Find All Obstacles In Scene")]
    void FindAllObstacles()
    {
        FindObstacleBaseList();
        FindTimeScaleObstacleList();
        FindWinTriggerBox();
    }

    void FindObstacleBaseList()
    {
        _obstacleList = new List<ObstacleBase>();
        ObstacleBase[] obstacleBases = FindObjectsOfType<ObstacleBase>();
        foreach (var obstacleBase in obstacleBases)
        {
            _obstacleList.Add(obstacleBase);
        }
    }
    
    void FindTimeScaleObstacleList()
    {
        _timeScaleObstacleList = new List<TimeScaleObstacle>();
        TimeScaleObstacle[] obstacleBases = FindObjectsOfType<TimeScaleObstacle>();
        foreach (var obstacleBase in obstacleBases)
        {
            _timeScaleObstacleList.Add(obstacleBase);
        }
    }
    
    void FindWinTriggerBox()
    {
        _winTriggerBox = FindObjectOfType<WinTriggerBox>();
    }
    
    public void ResetObstacle()
    {
        foreach (var obstacle in _obstacleList)
        {
            obstacle.ResetObstacle();
        }
        
        foreach (var timeScaleObstacle in _timeScaleObstacleList)
        {
            timeScaleObstacle.ResetObstacle();
        }
        
        _winTriggerBox.ResetObstacle();
    }
}
