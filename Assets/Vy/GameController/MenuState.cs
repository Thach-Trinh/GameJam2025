using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuState : AbstractGameState
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Menu menu;
    
    [SerializeField, Header("Debug")] private bool enableLog = false;
    private string logTag = $"{nameof(MenuState)} ";
    
    public override void OnExit()
    {
        menuCanvas.gameObject.SetActive(false);
        menu.Hide();
    }

    public override void OnEnter()
    {
        menuCanvas.gameObject.SetActive(true);
        menu.Show();
    }
}
