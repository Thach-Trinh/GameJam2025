using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Action<int> onEventTrigger;
    public void OnEventTrigger(int value) => onEventTrigger?.Invoke(value);
}
