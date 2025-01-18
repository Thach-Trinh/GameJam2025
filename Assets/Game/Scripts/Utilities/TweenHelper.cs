using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TweenHelper
{
    public static IEnumerator ChangeFloatValue(float startValue, float endValue, float duration,
        AnimationCurve curve, Action<float> action)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            float newValue = Mathf.Lerp(startValue, endValue, t);
            action?.Invoke(newValue);
            yield return null;
        }
        action?.Invoke(endValue);
    }
}
