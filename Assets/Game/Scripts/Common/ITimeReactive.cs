using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeReactive
{
    public void OnTimeScaleChanged(float timeScale);
}
