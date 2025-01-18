using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : AbstractGameState
{
  [SerializeField] private GameController gameController;
  [SerializeField] private Canvas introCanvas;
  
  public override void OnEnter()
  {
    introCanvas.gameObject.SetActive(true);
  }
  
  public override void OnExit()
  {
    introCanvas.gameObject.SetActive(false);
  }
}
