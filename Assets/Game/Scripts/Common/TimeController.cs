using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;
    private const float defaultTimeScale = 1f;
    public float curTimeScale;
    public float slowTimeScale;

    private void Awake()
    {
        Instance = this;
    }

    public void SetTimeScale(float timeScale)
    {
        this.curTimeScale = timeScale;

    }

    public void TriggerSlowMotion() => SetTimeScale(curTimeScale);
    public void TriggerNormalMotion() => SetTimeScale(defaultTimeScale);

}
