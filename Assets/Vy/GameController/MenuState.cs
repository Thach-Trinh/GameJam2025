using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : AbstractGameState
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Canvas menuCanvas;
    
    [SerializeField, Header("Debug")] private bool enableLog = false;
    private string logTag = $"{nameof(MenuState)} ";
    
    public void OnUserStart()
    {
        if (enableLog) Debug.Log($"{logTag} OnUserStart");
        gameController.SetState(gameController.LoadingState);
    }

    public override void OnExit()
    {
        menuCanvas.gameObject.SetActive(false);
    }

    public override void OnEnter()
    {
        menuCanvas.gameObject.SetActive(true);
    }
}
