using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimationEventHandler<T> : MonoBehaviour where T : Enum
{
    public Action<T> onEventTrigger;
    public void OnEventTrigger(T value) => onEventTrigger?.Invoke(value);
}
