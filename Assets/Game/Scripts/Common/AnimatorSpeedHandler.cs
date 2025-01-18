using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSpeedHandler : MonoBehaviour, ITimeReactive
{
    public Animator anim;
    //public float defaultSpeed = 1f;
    private void OnEnable()
    {
        TimeController.Instance.affectedObjects.Add(this);
    }

    private void OnDisable()
    {
        TimeController.Instance.affectedObjects.Remove(this);
    }

    public void OnTimeScaleChanged(float timeScale)
    {
        anim.speed = timeScale;
    }
}
