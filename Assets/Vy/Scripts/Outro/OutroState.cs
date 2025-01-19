using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroState : AbstractGameState
{
  [SerializeField] private GameController gameController;
  [SerializeField] private Canvas introCanvas;
  
  public override void OnEnter()
  {
    AudioController.Instance.StopSound(SoundName.GAMEPLAY);
    AudioController.Instance.PlaySound(SoundName.ALARM);
    AudioController.Instance.PlaySound(SoundName.OUTRO);
    introCanvas.gameObject.SetActive(true);
  }
  
  public override void OnExit()
  {
    introCanvas.gameObject.SetActive(false);
  }
}
