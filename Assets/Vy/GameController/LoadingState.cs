using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class LoadingState : AbstractGameState
{
   [SerializeField] private GameController gameController;
   [SerializeField] private Canvas loadingCanvas;
   [SerializeField] private Loading loading;
   [SerializeField] private int numberEnter = 0;
   public override async void OnEnter()
   {
      loadingCanvas.gameObject.SetActive(true);
      loading.Show();
      await UniTask.Delay(TimeSpan.FromSeconds(1f));
      if (numberEnter == 0)
         gameController.SetState(gameController.IntroState);
      else 
         gameController.SetState(gameController.OutroState);
      numberEnter++;
   }

   public override async void OnExit()
   {
      loading.Hide();
      await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
      loadingCanvas.gameObject.SetActive(false);
   }
}
