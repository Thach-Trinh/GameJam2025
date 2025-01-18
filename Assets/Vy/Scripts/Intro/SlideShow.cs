using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class SlideShow : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int slideIndex = 0;
    [SerializeField] private string stepAnimation = "Step";
    [SerializeField] private int finalSlideIndex = 3;

    private void OnEnable()
    {
        slideIndex = 0;
    }

    public async void NextStep()
    {
        if (slideIndex >= finalSlideIndex)
        {
            return;
        }
        
        slideIndex++;
        animator.SetInteger(stepAnimation, slideIndex);

        if (slideIndex >= finalSlideIndex)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1.1f));
            GameController.Instance.SetState(GameController.Instance.GameState);
        }
    }
}
