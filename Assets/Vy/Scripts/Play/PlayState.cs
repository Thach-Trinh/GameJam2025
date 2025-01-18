using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayState : AbstractGameState
{
   [SerializeField] private GameController gameController;
   [SerializeField] private Canvas playCanvas;
   public override async void OnEnter()
   {
      Debug.Log("PlayState OnEnter");
      gameController.Player.ChangeState(ActionType.Idle);
      gameController.Player.transform.position = gameController.PlayerSpawnPoint.position;
      gameController.CreateMap();
      // TODO: lang nghe player chet
      
      // TODO: lang nghe player win
      
      // TODO: lang nghe player vuot duoc chuong ngai vat
      
      playCanvas.gameObject.SetActive(true);

      await UniTask.Delay(TimeSpan.FromSeconds(2f));
      
      playCanvas.gameObject.SetActive(false);
      
      gameController.Player.ChangeState(ActionType.Run);
      // TODO: Bat dau game, nhan vat di chuyen
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
      playCanvas.gameObject.SetActive(false);
   }
}
