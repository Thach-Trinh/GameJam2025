using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTriggerBox : MonoBehaviour
{
    [SerializeField] private SpriteRenderer boxView;
    private ObstacleBase _obstacleBase;
    private bool _triggerredFlag;
    public Action _onPlayerTriggerObstacleBox;
    public void InitObstacleBox(ObstacleBase obstacleBase)
    {
        _obstacleBase = obstacleBase;
        _triggerredFlag = false;
        if (Application.isPlaying)
        {
            boxView.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggerredFlag) return;
        if (other.CompareTag(GameConstant.PLAYER_TAG))
        {
            _triggerredFlag = true;
            _onPlayerTriggerObstacleBox?.Invoke();
        }
    }
}
