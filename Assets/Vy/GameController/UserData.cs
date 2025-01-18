using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData : MonoBehaviour
{
    public int TotalObstacles = 10;
    public event Action<int, int> OnPassedObstaclesChanged;
    public event Action<int> OnDayChanged;
        
    [SerializeField] private int passedObstacles = 0;
    public int PassedObstacles
    {
        get => passedObstacles;
        set
        {
            passedObstacles = value;
            OnPassedObstaclesChanged?.Invoke(passedObstacles, TotalObstacles);
        }
    } 
    
    [SerializeField] private int day = 1;
    public int Day
    {
        get => day;
        set
        {
            day = value;
            OnDayChanged?.Invoke(day);
        }
    } 

    public void Restart()
    {
        PassedObstacles = 0;
    }
}
