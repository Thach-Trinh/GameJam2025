using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBaseGeneric<T> : ObstacleBase where T : ActionData
{
    public T actionData;
}
