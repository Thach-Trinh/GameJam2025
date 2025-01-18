using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Action<int> intEvent;
    public void OnIntEvent(int value)//dont rename this function
    {
        intEvent?.Invoke(value);
    }

}
