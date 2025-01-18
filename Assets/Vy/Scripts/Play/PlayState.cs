using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayState : AbstractGameState
{
   [SerializeField] private GameController gameController;
   public override void OnEnter()
   {
      Debug.Log("PlayState OnEnter");
      // TODO: lang nghe player chet
      
      // TODO: lang nghe player win
      
      // TODO: lang nghe player vuot duoc chuong ngai vat
   }
   
   public void OnPlayerPassedObstacle()
   {
      Debug.Log("Player passed obstacle");
      gameController.UserData.PassedObstacles++;
   }

   public void OnPlayerWin()
   {
      Debug.Log("Player win");
      gameController.SetState(gameController.OutroState);
   }
   
   public void OnPlayerDie()
   {
      Debug.Log("Player die");
      gameController.SetState(gameController.RestartState);
   }

   [ContextMenu("Trigger win")]
   public void TriggerWin()
   {
      OnPlayerWin();
   }
   
   [ContextMenu("Trigger die")]
   public void TriggerDie()
   {
      OnPlayerDie();
   }
   
   [ContextMenu("Trigger passed obstacle")]
   public void TriggerPassedObstacle()
   {
      OnPlayerPassedObstacle();
   }
 
   public override void OnExit()
   {
      Debug.Log("PlayState OnExit");
   }
}
