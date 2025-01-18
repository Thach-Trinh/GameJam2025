using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerShow = "Show";
    [SerializeField] private string triggerHide = "Hide";
    public void Hide()
    {
        animator.SetTrigger(triggerHide);
    }

    public void Show()
    {
        animator.SetTrigger(triggerShow);
    }
}
