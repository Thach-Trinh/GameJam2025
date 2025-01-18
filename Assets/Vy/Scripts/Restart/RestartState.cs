using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RestartState : AbstractGameState
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Canvas restartCanvas;

    public override void OnExit()
    {
        restartCanvas.gameObject.SetActive(false);
    }

    public override async void OnEnter()
    {
        restartCanvas.gameObject.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        gameController.DestroyMap();
    }
}
