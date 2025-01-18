using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriggerBox : MonoBehaviour
{
    [SerializeField] private ObstacleTriggerBox _obstacleTriggerBox;

    public void ResetObstacle()
    {
        InitWinTriggerBox();
    }

    void InitWinTriggerBox()
    {
        _obstacleTriggerBox.InitObstacleBox();
        _obstacleTriggerBox._onPlayerTriggerObstacleBox = null;
        _obstacleTriggerBox._onPlayerTriggerObstacleBox += TriggerWinGame;
    }
    
    private void TriggerWinGame()
    {
        Debug.Log("Player entered win trigger box");
        if (GameController.Instance.CurrentState == GameController.Instance.GameState)
        {
            var playState = GameController.Instance.GameState as PlayState;
            playState?.OnPlayerWin();
        }
    }
}
