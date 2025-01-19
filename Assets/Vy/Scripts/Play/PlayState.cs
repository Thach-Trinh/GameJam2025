using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayState : AbstractGameState
{
   [SerializeField] private GameController gameController;
   [SerializeField] private Canvas playCanvas;
   public override async void OnEnter()
   {
      Player.Instance.anim.enabled = false;
        Player.Instance.anim.gameObject.SetActive(true);
        Player.Instance.happyPoint = 0;
        Debug.Log("PlayState OnEnter");
        CameraController.Instance.SetPos(Player.Instance.transform.position);
      gameController.Player.ChangeState(ActionType.Idle);
      gameController.Player.transform.position = gameController.PlayerSpawnPoint.position;
      gameController.CreateMap();
      TimeController.Instance.SetTimeScale(1);
      // TODO: lang nghe player chet
      
      // TODO: lang nghe player win
      
      // TODO: lang nghe player vuot duoc chuong ngai vat
      
      playCanvas.gameObject.SetActive(true);
      AudioController.Instance.PlaySound(SoundName.ALARM);
      await UniTask.Delay(TimeSpan.FromSeconds(2f));
      TimeController.Instance.GetAllAffectedObjects();
      playCanvas.gameObject.SetActive(false);
      await UniTask.Delay(TimeSpan.FromSeconds(0.5));
      Player.Instance.anim.enabled = true;
      gameController.Player.ChangeState(ActionType.Run);
      // TODO: Bat dau game, nhan vat di chuyen
   }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Player.Instance.CustomUpdate(TimeController.Instance.curTimeScale);
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
        CameraController.Instance.Follow(Player.Instance.transform.position);
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
      Player.Instance.anim.gameObject.SetActive(false);
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
