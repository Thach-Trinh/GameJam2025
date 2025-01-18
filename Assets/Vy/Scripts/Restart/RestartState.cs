using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartState : AbstractGameState
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Canvas restartCanvas;

    public override void OnExit()
    {
        restartCanvas.gameObject.SetActive(false);
    }

    public override void OnEnter()
    {
        restartCanvas.gameObject.SetActive(true);
    }
}
